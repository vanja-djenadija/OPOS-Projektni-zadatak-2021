using Microsoft.WindowsAPICodePack.Dialogs;
//using MultimediaDataProcessing;
using MyTaskScheduler;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _2_GraphicalUserInterfaceApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreateNewTaskWindow : Window
    {

        private readonly LinkedList<string> _tasks = new();
        public MyTask? task { get; set; }

        public CreateNewTaskWindow(bool priority)
        {
            InitializeComponent();
            Configure();
            priorityCB.ItemsSource = Enum.GetValues(typeof(Priority));
            priorityCB.SelectedItem = Priority.Medium;
            taskTypeCB.ItemsSource = _tasks;
            taskTypeCB.SelectedIndex = 0;
            DateTimePicker.Maximum = new DateTime(2023, 1, 1);
            if (!priority)
                priorityCB.IsEnabled = false;
        }

        private void Configure()
        {
            _tasks.AddLast(typeof(ImageSharpeningTask).Name);
            //_tasks.AddLast("Image Blurring");
        }

        private void AddResourceBtn_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = false,
                Multiselect = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var files = dialog.FileNames;
                files.ToList().ForEach(file => resourcesLB.Items.Add(file.ToString()));
            }
        }

        private void AddOutputFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new();
            if (dialog.ShowDialog() == true)
                outputFolderLbl.Content = dialog.SelectedPath;
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DateTimePicker.Value == null || maxExecTimeTB.Text.Length == 0 || maxDegreeOfParallelismTB.Text.Length == 0 || resourcesLB.Items.Count == 0 || outputFolderLbl.Content == null || outputFolderLbl.Content.Equals(""))
            {
                System.Windows.MessageBox.Show("Missing parameteres.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            try
            {
                int maxExecTime = Int32.Parse(maxExecTimeTB.Text);
                int maxDegParallelism = Int32.Parse(maxDegreeOfParallelismTB.Text);
                if (DateTimePicker.Value < DateTime.Now || maxExecTime < 0 || maxDegParallelism < 0)
                    throw new Exception();

                if (maxDegParallelism > Environment.ProcessorCount)
                    maxDegParallelism = Environment.ProcessorCount;

                LinkedList<Resource> resources = new();
                foreach (string resource in resourcesLB.Items)
                    resources.AddLast(new FileResource(resource));

                Priority priority = (Priority)Enum.Parse(typeof(Priority), priorityCB.SelectedItem.ToString());
                string taskString = taskTypeCB.SelectedItem.ToString();
                switch (taskString)
                {
                    case "ImageSharpeningTask":
                        task = new ImageSharpeningTask(priority, maxExecTime, (DateTime)DateTimePicker.Value, maxDegParallelism, resources, outputFolderLbl.Content.ToString());
                        task.Resources.ToList().ForEach(r => r.Task = task);
                        break;
                }
            }
            catch (Exception exception)
            {
                System.Windows.MessageBox.Show("Invalid parameteres.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            System.Windows.MessageBox.Show("Task added.", "Information.", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Hide();
        }
    }

}

