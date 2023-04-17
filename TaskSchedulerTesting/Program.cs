// See https://aka.ms/new-console-template for more information
using MyTaskScheduler;
Console.WriteLine(DateTime.Now);
string imagePath1 = ".\\airplane.jpg";
string imagePath2 = ".\\vanja1.jpg";
string imagePath3 = ".\\vanja2.jpg";
Resource resource1 = new FileResource(imagePath1);
Resource resource2 = new FileResource(imagePath2);
Resource resource3 = new FileResource(imagePath3);

LinkedList<Resource> list = new LinkedList<Resource>();
list.AddFirst(resource1);
//list.AddFirst(resource2);
//list.AddFirst(resource3);
string outputFolder = ".";

ImageSharpeningTask i1 = new ImageSharpeningTask(Priority.Low, 1_000_000_000_000, new DateTime(2023, 07, 11, 19, 19, 59), 6, list, outputFolder);
//i1.Serialize();
//MyTaskScheduler.TaskScheduler scheduler = new MyTaskScheduler.TaskScheduler(6, 1, true, true);
//scheduler.Add(i1);
//scheduler.Serialize();
//scheduler.Start();
//Thread.Sleep(90_000);
//MyTaskScheduler.TaskScheduler scheduler = MyTaskScheduler.TaskScheduler.Deserialize();
//Console.WriteLine(scheduler.MaxNumberOfTasks);
//Console.WriteLine(scheduler.Started);
//Console.WriteLine(scheduler.NumberOfCores);
//Console.WriteLine(scheduler.Tasks.Dequeue());
//Thread.Sleep(120_000);
//scheduler.Add(SpecialTask.Deserialize()); // Type.GetType(STRING)
//Thread.Sleep(200);
//scheduler.Add(t3);
//
//Thread.Sleep(200);
//scheduler.Add(t2);
MyTaskScheduler.TaskScheduler scheduler = MyTaskScheduler.TaskScheduler.Deserialize();
Console.WriteLine(scheduler);
scheduler.Start();
Thread.Sleep(90_000);
//Thread.Sleep(200);

//scheduler.Add(t4);