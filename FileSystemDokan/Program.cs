// See https://aka.ms/new-console-template for more information
using DokanNet;
using System.Diagnostics;
using System.Drawing;
using MyTaskScheduler;
using System.Collections.Generic;
using System.Threading;
using System.IO;

Dictionary<string, bool> filesProcessed = new();

MyTaskScheduler.TaskScheduler scheduler = new(4, 10, false, false);
scheduler.Start();
new Thread(() =>
{
    while (true)
    {
        Thread.Sleep(500);
        string folder = "A:\\input\\";
        var files = Directory.GetFiles(folder);
        foreach (var file in files)
        {
            if (!filesProcessed.ContainsKey(file))
            {
                Thread.Sleep(100);
                LinkedList<Resource> list = new LinkedList<Resource>();
                list.AddFirst(new FileResource(file));
                scheduler.Add(new ImageSharpeningTask(Priority.Low, 1_000_000_000_000, new DateTime(2023, 07, 11, 19, 19, 59), 6, list, "A:\\output\\"));
                filesProcessed.Add(file, true);
            }
        }
    }
}).Start();

new FileSystemDokan.FileSystem().Mount("A:\\", DokanOptions.DebugMode | DokanOptions.StderrOutput);