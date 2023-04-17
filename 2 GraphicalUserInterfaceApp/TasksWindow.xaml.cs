using MyTaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _2_GraphicalUserInterfaceApp
{
    public partial class TasksWindow : Window
    {
        private readonly int autoSaveMilliseconds = 10_000;
        private MyTaskScheduler.TaskScheduler scheduler;
        private readonly bool priority;

        private readonly Dictionary<string, Action> tasks = new();

        // used when restoring previous version of task scheduler
        public TasksWindow()
        {
            InitializeComponent();
            scheduler = MyTaskScheduler.TaskScheduler.Deserialize();
            Restore();
            scheduler.Start();
            new Thread(Autosave) { IsBackground = true }.Start();
        }

        // used when creating new version of task scheduler
        public TasksWindow(int cores, int numberOfTasks, bool priority, bool preemptive)
        {
            InitializeComponent();
            this.priority = priority;
            scheduler = new MyTaskScheduler.TaskScheduler(cores, numberOfTasks, priority, preemptive);
            scheduler.Start();
            new Thread(Autosave) { IsBackground = true }.Start();
        }

        // displaying restored state of task scheduler on GUI
        private void Restore()
        {
            int i = 0;
            foreach (var pair in scheduler.Tasks.UnorderedItems)
            {
                if (i < scheduler.MaxNumberOfTasks)
                    AddTaskToStackPanel(pair.Element, true);
                else
                    AddTaskToStackPanel(pair.Element, false);
                i++;
            }
        }

        private void Autosave()
        {
            while (true)
            {
                Save();
                Thread.Sleep(autoSaveMilliseconds);
            }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CreateNewTask_Click(object sender, RoutedEventArgs e)
        {
            CreateNewTaskWindow win = new CreateNewTaskWindow(priority);
            if (win.ShowDialog() == false && win.task != null)
                AddTaskToStackPanel(win.task, false);
        }

        private void AddTaskToStackPanel(MyTask task, bool reload)
        {
            if (task == null)
                System.Windows.MessageBox.Show("Task is null.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            TaskProgressBar taskProgressBar = new TaskProgressBar(task.Id, task, scheduler);
            this.Dispatcher.Invoke(() => taskProgressBar.taskPB.Value = task.ProgressBarPercentage); // TODO: set to calculated percentage, not saved
            if (reload)
            {
                taskProgressBar.startBtn.IsEnabled = false;
                taskProgressBar.removeBtn.IsEnabled = false;
                taskProgressBar.pauseBtn.IsEnabled = true;
                taskProgressBar.cancelBtn.IsEnabled = true;
            }
            taskProgressBar.RemoveProgressBar = () => tasksSP.Children.Remove(taskProgressBar);
            tasksSP.Children.Add(taskProgressBar);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            scheduler.Serialize();
        }
    }
}
