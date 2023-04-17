using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _2_GraphicalUserInterfaceApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            coresCB.ItemsSource = Enumerable.Range(1, Environment.ProcessorCount);
            coresCB.SelectedIndex = 0;
            Restore();
        }

        // restore previous version of task scheduler if there is any
        private void Restore()
        {
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] Files = dirInfo.GetFiles("*.json");

            foreach (FileInfo file in Files)
            {
                if (file.Name.StartsWith("TaskScheduler"))
                {
                    if (MessageBox.Show("Do you want to restore previous version of task scheduler?", "Restore", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                    {
                        TasksWindow tasksWindow = new();
                        this.Hide();
                        tasksWindow.Show();
                    }
                }
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (maxNoConcurrentTasksTB.Text.Length != 0)
            {
                try
                {
                    int cores = Int32.Parse(coresCB.SelectedItem.ToString());
                    int tasks = Int32.Parse(maxNoConcurrentTasksTB.Text);
                    if (tasks < 0)
                        throw new Exception();
                    bool priority = (bool)priorityRB.IsChecked;
                    bool preemptive = (bool)preemptiveRB.IsChecked;
                    this.Hide();
                    TasksWindow tasksWindow = new(cores, tasks, priority, preemptive);
                    tasksWindow.Show();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Invalid parameteres.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Missing parameteres.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
