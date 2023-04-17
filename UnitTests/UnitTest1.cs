using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PIPTest()
        {
            Resource resource = new SpecialResource();
            LinkedList<Resource> list = new LinkedList<Resource>();
            list.AddFirst(resource);
            LinkedList<Resource> list2 = new LinkedList<Resource>();
            list2.AddFirst(new SpecialResource());
            SpecialTask t1 = new SpecialTask(Priority.Low, 100, new DateTime(2023, 07, 11, 19, 19, 59), 4, "Task1", list);
            SpecialTask t2 = new SpecialTask(Priority.Medium, 100, new DateTime(2023, 07, 11, 19, 19, 59), 4, "Task2", list2);
            SpecialTask t3 = new SpecialTask(Priority.High, 100, new DateTime(2023, 07, 11, 19, 19, 59), 4, "Task3", list);
            resource.Task = t1;

            MyTaskScheduler.TaskScheduler scheduler = new MyTaskScheduler.TaskScheduler(4, 1, true, true);
            scheduler.Add(t1);
            new Thread(scheduler.Start).Start();
            Thread.Sleep(200);
            scheduler.Add(t2);
            Thread.Sleep(200);
            scheduler.Add(t3);
            while (!(t1.Terminated && t2.Terminated && t3.Terminated)) ;
            Assert.IsTrue((t1.DateTimeFinished < t3.DateTimeFinished) && (t3.DateTimeFinished < t2.DateTimeFinished));
        }

        // TODO
        [TestMethod]
        public void DeadlockTest()
        {

        }

        [TestMethod]
        public void DeadlineTest()
        {
            MyTaskScheduler.TaskScheduler scheduler = new MyTaskScheduler.TaskScheduler(4, 1, true, true);
            LinkedList<Resource> list = new LinkedList<Resource>();
            list.AddFirst(new FileResource(".\\airplane.jpg"));
            SpecialTask t1 = new SpecialTask(Priority.Low, 100, DateTime.Now + new System.TimeSpan(0, 0, 3), 4, "Task1", list);
            scheduler.Start();
            scheduler.Add(t1);
            while (!t1.Terminated) ;
            Assert.IsTrue(t1.Terminated);
        }

        [TestMethod]
        public void MaxExecTimeTest()
        {
            MyTaskScheduler.TaskScheduler scheduler = new MyTaskScheduler.TaskScheduler(4, 1, true, true);
            SpecialTask t1 = new SpecialTask(Priority.Low, 1, DateTime.Now + new System.TimeSpan(0, 0, 3), 4, "Task1", new LinkedList<Resource>());
            scheduler.Start();
            scheduler.Add(t1);
            while (!t1.Terminated) ;
            Assert.IsTrue(t1.Terminated);
        }

        [TestMethod]
        public void SharedResourceTest()
        {
            Resource resource = new SpecialResource();
            LinkedList<Resource> list = new LinkedList<Resource>();
            list.AddFirst(resource);
            SpecialTask t1 = new SpecialTask(Priority.Low, 100, new DateTime(2023, 07, 11, 19, 19, 59), 4, "Task1", list);
            SpecialTask t3 = new SpecialTask(Priority.High, 100, new DateTime(2023, 07, 11, 19, 19, 59), 4, "Task3", list);
            resource.Task = t1;

            MyTaskScheduler.TaskScheduler scheduler = new MyTaskScheduler.TaskScheduler(4, 1, true, true);
            scheduler.Add(t1);
            new Thread(scheduler.Start).Start();
            Thread.Sleep(200);
            scheduler.Add(t3);
            while (!(t1.Terminated && t3.Terminated)) ;
            Assert.IsTrue(t1.DateTimeFinished < t3.DateTimeFinished);
        }

        [TestMethod]
        public void SerializeTaskTest()
        {
            LinkedList<Resource> list = new LinkedList<Resource>();
            list.AddFirst(new FileResource(".\\airplane.jpg"));
            ImageSharpeningTask i1 = new ImageSharpeningTask(Priority.Low, 1_000_000_000_000, new DateTime(2023, 07, 11, 19, 19, 59), 6, list, ".");
            i1.Serialize();
            ImageSharpeningTask deserializedTask = (ImageSharpeningTask)MyTask.Deserialize(i1.Id + ".json");
            Assert.IsTrue(i1.Equals(deserializedTask));
        }
    }
}