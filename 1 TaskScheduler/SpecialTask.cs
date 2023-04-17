using MyTaskScheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MyTaskScheduler
{
    public class SpecialTask : MyTask
    {
        public String Name { get; set; }
        public SpecialTask(Priority Priority, long MaxExecutionTime, DateTime Deadline, int NumberOfCores, String Name, LinkedList<Resource> Resources) : base(null, Priority, MaxExecutionTime, Deadline, NumberOfCores, Resources)
        {
            this.Name = Name;
            Action = () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Terminated)
                    {
                        break;
                    }
                    if (Paused)
                    {
                        lock (this)
                        {
                            Monitor.Wait(this);
                        }
                    }
                    Console.WriteLine(Name + " " + i);
                    Thread.Sleep(500);
                }
            };
        }
        public override string ToString()
        {
            return Name + " " + Priority;
        }
        //public override void Serialize()
        //{
        //    string fileName = "TaskScheduler.json";
        //    string jsonString = JsonSerializer.Serialize(this);
        //    File.WriteAllText(fileName, jsonString);
        //}
        //
        //public static MyTask Deserialize()
        //{
        //    string fileName = "TaskScheduler.json";
        //    string jsonString = File.ReadAllText(fileName);
        //    return JsonSerializer.Deserialize<SpecialTask>(jsonString);
        //}
    }
}
