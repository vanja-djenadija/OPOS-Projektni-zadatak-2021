using MyTaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2_GraphicalUserInterfaceApp
{
    public partial class TaskProgressBar : UserControl
    {
        private MyTask task;
        private MyTaskScheduler.TaskScheduler scheduler;
        public Action RemoveProgressBar { get; set; }
        public TaskProgressBar(string taskId, MyTask task, MyTaskScheduler.TaskScheduler scheduler)
        {
            InitializeComponent();
            taskIdLbl.Content = taskId;
            this.task = task;
            this.scheduler = scheduler;
            task.RemoveTask = () => this.Dispatcher.Invoke(() => removeBtn.IsEnabled = true);
            task.UpdateProgressBar = () => this.Dispatcher.Invoke(() => taskPB.Value = task.ProgressBarPercentage);
            taskPB.Minimum = 0.0;
            taskPB.Maximum = 1.0;
            cancelBtn.IsEnabled = false;
            resumeBtn.IsEnabled = false;
            pauseBtn.IsEnabled = false;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler.Add(task);
            startBtn.IsEnabled = false;
            removeBtn.IsEnabled = false;
            pauseBtn.IsEnabled = true;
            cancelBtn.IsEnabled = true;
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            pauseBtn.IsEnabled = false;
            resumeBtn.IsEnabled = true;
            task.Pause();
        }

        private void ResumeBtn_Click(object sender, RoutedEventArgs e)
        {
            pauseBtn.IsEnabled = true;
            resumeBtn.IsEnabled = false;
            task.Continue();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            RemoveProgressBar();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            cancelBtn.IsEnabled = false;
            startBtn.IsEnabled = false;
            pauseBtn.IsEnabled = false;
            resumeBtn.IsEnabled = false;
            removeBtn.IsEnabled = true;
            task.Stop();
        }
    }
}
