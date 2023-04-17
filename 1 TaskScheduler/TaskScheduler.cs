using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

namespace MyTaskScheduler
{
    public class TaskScheduler
    {

        private static readonly int MaxNumberOfCores = Environment.ProcessorCount;
        [JsonIgnore]
        public PriorityQueue<MyTask, Priority> Tasks { get; set; } = new();
        [JsonProperty]
        private LinkedList<MyTask> TasksWrapper { get; set; } = new();
        [JsonProperty]
        public LinkedList<MyTask> RunningTasks { get; set; } = new();
        [JsonProperty]
        private HashSet<Resource> Resources { get; set; } = new();

        readonly object LOCK = new();

        public int NumberOfCores { get; private set; }

        public int MaxNumberOfTasks { get; private set; }

        public bool Started { get; private set; }

        public bool PriorityScheduling { get; private set; }

        public bool PreemptiveScheduling { get; private set; }

        public TaskScheduler(int numberOfCores, int maxNumberOfTasks, bool priorityScheduling, bool preemptiveScheduling)
        {
            NumberOfCores = numberOfCores;
            MaxNumberOfTasks = maxNumberOfTasks;
            PriorityScheduling = priorityScheduling;
            PreemptiveScheduling = preemptiveScheduling;
            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)(Math.Pow(2, numberOfCores) - 1);
        }


        public void Add(MyTask task)
        {
            lock (LOCK)
            {
                Tasks.Enqueue(task, task.Priority);
                task.Resources.ToList().ForEach(r => Resources.Add(r));
                Monitor.Pulse(LOCK);
            }
        }

        private MyTask Remove()
        {
            MyTask task;
            lock (LOCK)
            {
                task = Tasks.Dequeue();
            }
            return task;
        }


        public void Start()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (Tasks.Count > 0 && RunningTasks.Count < MaxNumberOfTasks)
                    {
                        MyTask task = Tasks.Peek();
                        if (!task.Paused && task.Resources.Any(r => !checkIfNotTaken(r, task)))
                            continue;

                        Remove();
                        lock (LOCK)
                            RunningTasks.AddLast(task);
                        if (!task.Paused)
                        {
                            Thread taskThread = new Thread(() =>
                            {
                                task.Start();
                                // 1. OCT After task has finished, remove all resources from scheduler
                                task.Resources.ToList().ForEach(resource => Resources.Remove(resource));
                                lock (LOCK)
                                {
                                    RunningTasks.Remove(task);
                                    Monitor.Pulse(LOCK);
                                }
                            })
                            { IsBackground = true };
                            taskThread.Start();
                        }
                        else
                            task.Continue();
                    }
                    else if (Tasks.Count > 0 && PreemptiveScheduling) // if MaxNumberOfTasks are executing
                    {
                        MyTask nextTask = Tasks.Peek();
                        lock (LOCK)
                        {
                            foreach (MyTask runningTask in RunningTasks)
                            {
                                if (nextTask.Priority < runningTask.Priority) // if task has higher priority than running task
                                {
                                    // Task with higher priority is running only if its all resources are available
                                    if (nextTask.Resources.All(r => checkIfNotTaken(r, nextTask)))
                                    {
                                        runningTask.Pause();
                                        RunningTasks.Remove(runningTask);
                                        Add(runningTask);
                                        break;
                                    }
                                    else // If higher-priority task has some resources which are already locked by another task
                                    {
                                        // PIP
                                        // Check for more locked resources by different tasks
                                        foreach (Resource resource in nextTask.Resources)
                                        {
                                            Resource foundResource;
                                            if (Resources.TryGetValue(resource, out foundResource) && foundResource.Task.Paused)
                                            {
                                                foundResource.Task.Priority = Priority.High;
                                                var tasks = Tasks.UnorderedItems.Where(t => !t.Element.Equals(foundResource.Task));
                                                Tasks = new();
                                                Tasks.Enqueue(foundResource.Task, Priority.High);
                                                tasks.ToList().ForEach(t => Tasks.Enqueue(t.Element, t.Priority));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Monitor.Wait(LOCK);
                                    break;
                                }
                            }
                        }
                    }
                    else
                        lock (LOCK)
                            Monitor.Wait(LOCK);
                }

            })
            { IsBackground = true }.Start();

            // Check if exceeded maximum execution time or deadline
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    lock (LOCK)
                    {
                        for (int i = 0; i < RunningTasks.Count; i++)
                        {
                            MyTask task = RunningTasks.ElementAt(i);
                            if (task.Stopwatch.ElapsedMilliseconds / 1000 > task.MaxExecutionTime || DateTime.Now >= task.Deadline)
                                task.Stop();

                        }
                    }
                }
            })
            { IsBackground = true }.Start();
        }

        private bool checkIfNotTaken(Resource r, MyTask nextTask)
        {
            if (Resources.Contains(r))
            {
                Resource existingResource;
                Resources.TryGetValue(r, out existingResource);
                return !existingResource.Taken || existingResource.Task.Equals(nextTask);
            }
            return true;
        }

        public virtual void Serialize()
        {
            string fileName = "TaskScheduler.json";
            File.Delete(fileName);
            Tasks.UnorderedItems.ToList().ForEach(pair => TasksWrapper.AddLast(pair.Element));
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            string jsonString = JsonConvert.SerializeObject(this, settings);
            File.WriteAllText(fileName, jsonString);
        }

        public static TaskScheduler Deserialize()
        {
            string fileName = "TaskScheduler.json";
            string jsonString = File.ReadAllText(fileName);
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
            TaskScheduler scheduler = JsonConvert.DeserializeObject<TaskScheduler>(jsonString, settings);
            scheduler.RunningTasks.ToList().ForEach(task => scheduler.Tasks.Enqueue(task, task.Priority));
            scheduler.RunningTasks.Clear();
            // adding tasks from List to PriorityQueue
            scheduler.TasksWrapper.ToList().ForEach(task => scheduler.Tasks.Enqueue(task, task.Priority));
            // making sure that the next task has correct Id
            int numberOfTasks = scheduler.Tasks.Count;
            MyTask.setCount(++numberOfTasks);
            return scheduler;
        }

        public override string ToString()
        {
            return Tasks + " " + RunningTasks.ToString() + " " + Resources.Count() + " " + NumberOfCores + " " + MaxNumberOfTasks;
        }

    }
}

