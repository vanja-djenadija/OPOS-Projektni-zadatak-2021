using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using MyTaskScheduler;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace MyTaskScheduler
{
    /* Source: https://epochabuse.com/color-image-smoothing-and-sharpening/ */
    public class ImageSharpeningTask : MyTask
    {
        public string OutputFolder { get; set; }
        private int processedRows = 0;
        private int maximumRows = 0;

        public ImageSharpeningTask(Priority Priority, long MaxExecutionTime, DateTime Deadline, int NumberOfCores, LinkedList<Resource> Resources, string outputFolder) : base(null, Priority, MaxExecutionTime, Deadline, NumberOfCores, Resources)
        {
            Action = this.ImageSharpen;
            OutputFolder = outputFolder;
        }

        private void ImageSharpen()
        {
            CalculateMaximumRows();
            CalculateProcessedRows();
            if (Resources.Count == 1)
            {
                ExecuteSingleTask(Resources.First(), NumberOfCores, 0);
            }
            else // Parallel processing of multiple resources
            {
                Parallel.For(0, Resources.Count, new ParallelOptions { MaxDegreeOfParallelism = NumberOfCores }, i =>
                {
                    ExecuteSingleTask(Resources.ElementAt(i), 1, i);
                });
            }
        }

        private void CalculateMaximumRows()
        {
            foreach (var resource in Resources)
            {
                using (Stream stream = resource.GetResource())
                {
                    maximumRows += new Bitmap(stream).Height - 2 * (Kernels.Laplacian.Length - 1);
                }
            }
        }

        private void CalculateProcessedRows()
        {
            for (int i = 0; i < ResourcesProcessed.Count; i++)
            {
                if (ResourcesProcessed[i])
                {
                    using (Stream stream = Resources.ElementAt(i).GetResource())
                    {
                        processedRows += new Bitmap(stream).Height - 2 * (Kernels.Laplacian.Length - 1);
                    }
                }
            }
        }

        private void ExecuteSingleTask(Resource resource, int numberOfCores, int index)
        {
            if (!ResourcesProcessed[index])
            {
                Stream stream = resource.GetResource();
                string fileName = Path.GetFileName(((FileResource)resource).Path);
                Bitmap image = new Bitmap(stream);
                Bitmap sharpImage = UnsafeImageSharpen(image, numberOfCores);
                string outputPath = Path.Combine(OutputFolder, "SharpImage_" + fileName);
                if (!Terminated)
                    sharpImage.Save(outputPath);
                ResourcesProcessed[index] = true;
                stream.Close();
            }
        }

        Bitmap UnsafeImageSharpen(Bitmap image, int numberOfCores)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int width = image.Width;
            int height = image.Height;
            Bitmap clonedImage = (Bitmap)image.Clone();
            BitmapData srcImage = clonedImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, image.PixelFormat);
            BitmapData destImage = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, image.PixelFormat);

            int dim = Kernels.Laplacian.Length;
            int size = dim - 1;

            unsafe
            {
                byte* buffer = (byte*)(void*)srcImage.Scan0;
                byte* result = (byte*)(void*)destImage.Scan0;
                // optimized for cache hits
                Parallel.For(size, height - size, new ParallelOptions { MaxDegreeOfParallelism = numberOfCores }, i => // row
                {
                    if (Terminated)
                        return;
                    if (Paused)
                        lock (this)
                            Monitor.Wait(this);

                    for (int j = size; j < width - size; j++) // column
                    {
                        int p = j * dim + i * srcImage.Stride;
                        for (int k = 0; k < dim; k++)
                        {
                            double val = 0d;
                            for (int xkernel = -1; xkernel < 2; xkernel++)
                            {
                                for (int ykernel = -1; ykernel < 2; ykernel++)
                                {
                                    int kernel_p = k + p + xkernel * 3 + ykernel * srcImage.Stride;
                                    val += buffer[kernel_p] * Kernels.Laplacian[xkernel + 1, ykernel + 1];
                                }
                            }
                            val = val > 0 ? val : 0;
                            result[p + k] = (byte)((val + buffer[p + k]) > 255 ? 255 : (val + buffer[p + k]));
                        }
                    }
                    Interlocked.Increment(ref processedRows);
                    Interlocked.Exchange(ref ProgressBarPercentage, 1.0 * processedRows / maximumRows);
                    if (UpdateProgressBar != null) UpdateProgressBar();
                });
            }
            image.UnlockBits(destImage);
            clonedImage.UnlockBits(srcImage);
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            return image;
        }

        private Bitmap ImageSharpen(Bitmap image)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int width = image.Width;
            int height = image.Height;
            BitmapData image_data = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, image.PixelFormat);
            int bytes = image_data.Stride * image_data.Height;
            byte[] buffer = new byte[bytes];
            byte[] result = new byte[bytes];
            Marshal.Copy(image_data.Scan0, buffer, 0, bytes);
            image.UnlockBits(image_data);
            int dim = Kernels.Laplacian.Length;
            int size = dim - 1;

            // TODO: provjeriti iteriranje
            for (int i = size; i < width - size; i++) // width
            {
                for (int j = size; j < height - size; j++) // height
                {

                    int p = i * dim + j * image_data.Stride;
                    for (int k = 0; k < dim; k++)
                    {
                        double val = 0d;
                        for (int xkernel = -1; xkernel < 2; xkernel++)
                        {
                            for (int ykernel = -1; ykernel < 2; ykernel++)
                            {
                                int kernel_p = k + p + xkernel * 3 + ykernel * image_data.Stride;
                                val += buffer[kernel_p] * Kernels.Laplacian[xkernel + 1, ykernel + 1];
                            }
                        }
                        val = val > 0 ? val : 0;
                        result[p + k] = (byte)((val + buffer[p + k]) > 255 ? 255 : (val + buffer[p + k]));
                    }
                }
            }
            Bitmap res_img = new(width, height);
            BitmapData res_data = res_img.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, image.PixelFormat);
            Marshal.Copy(result, 0, res_data.Scan0, bytes);
            res_img.UnlockBits(res_data);
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            return res_img;
        }
    }
}
