using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MyTaskScheduler
{
    public enum Priority
    {
        Low = 3, Medium = 2, High = 1
    }

    public class MyTask
    {

        // TODO: Deserijalizacija kreiranje novog Taska treba nastaviti od posljednjeg count-a
        private static int count = 1;

        public string Id { get; set; }
        public bool Terminated { get; set; } = false;
        public bool Paused { get; set; } = false;

        public bool Started { get; set; } = false;

        [JsonIgnore]
        public Action Action { get; set; }
        public Priority Priority { get; set; }
        public DateTime Deadline { get; private set; } // Deadline by which the task must be completed or interrupted
        public LinkedList<Resource> Resources { get; private set; }
        public List<bool> ResourcesProcessed { get; set; } = new();
        public int NumberOfCores { get; private set; } // Level of parallelism

        public long MaxExecutionTime { get; private set; } // in Seconds

        public Stopwatch Stopwatch { get; set; } = new();

        public DateTime DateTimeFinished { get; set; }

        [JsonIgnore]
        public Action RemoveTask { get; set; }
        [JsonIgnore]
        public Action UpdateProgressBar { get; set; }

        public double ProgressBarPercentage;

        public MyTask(Action Action, Priority Priority, long MaxExecutionTime, DateTime Deadline, int NumberOfCores, LinkedList<Resource> Resources)
        {
            Id = "Task " + count++;
            this.Action = Action;
            this.Priority = Priority;
            this.MaxExecutionTime = MaxExecutionTime;
            this.Deadline = Deadline;
            this.NumberOfCores = NumberOfCores;
            this.Resources = Resources;
            for (int i = 0; i < Resources.Count; i++)
                ResourcesProcessed.Add(false);

        }

        public void Start()
        {
            // lock resources
            Resources.ToList().ForEach(resource =>
            {
                resource.Taken = true;
                resource.Task = this;
            });
            Stopwatch.Start();
            Started = true;
            Action();
            // unlock resources
            Resources.ToList().ForEach(resource =>
            {
                resource.Taken = false;
                resource.Task = null;
            });
            DateTimeFinished = DateTime.Now;
            Terminated = true;
            // Removing progress bar of terminated task from GUI
            if (RemoveTask != null) RemoveTask();
        }

        public void Continue()
        {
            lock (this)
                Monitor.PulseAll(this);
            Paused = false;
            Started = true;
            Stopwatch.Start();
        }

        public void Pause()
        {
            Paused = true;
            Stopwatch.Stop();
        }

        public void Stop()
        {
            Terminated = true;
            Started = false;
        }

        public override string ToString()
        {
            return Id + " " + Priority + " " + Terminated + " " + Resources + " " + ResourcesProcessed;
        }


        public virtual void Serialize()
        {
            string fileName = Id + ".json";
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
            string jsonString = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(fileName, jsonString);
        }

        public static MyTask Deserialize(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
            return JsonConvert.DeserializeObject<MyTask>(jsonString, settings);
        }

        public override bool Equals(object obj)
        {
            var task = obj as MyTask;
            if (task == null)
                return false;

            return task.Id.Equals(this.Id);
        }

        public static void setCount(int c)
        {
            count = c;
        }
    }
}
