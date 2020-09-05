using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread
{
    class Program
    {
        public static int SharedResource { get; set; }
        public static object Locker = 0;

        static void Main(string[] args)
        {
            // StartThreadExample();

            // StartTaskExample();
            // StartAsyncExample();
            StartLockExample();
            Console.WriteLine("DOING MY STUFF");
        }

        private static void StartThreadExample()
        {
            // IterationExample();

            new Example("one_thousand");
            new Example("ten_thousand");
            // new Example("fifty_thousand");
            // new Example("one_hundred_thousand");
            var ex = new Example("three_millions", true);
        }

        private static void IterationExample()
        {
            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("111");
                    Thread.Sleep(100);
                }
            });

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("222");
                    Thread.Sleep(100);
                }
            });

            t1.Start();
            t2.Start();
        }

        private static void StartTaskExample()
        {
            Task<string> taskWithReturn = Task.Run(MyReturnTask);
            Console.WriteLine(taskWithReturn.Result);
            
            Task task = MyTask();
            Console.WriteLine($"Task status: {task.Status}");
            Thread.Sleep(1000);
            
            task.Start();
            Console.WriteLine($"Task status: {task.Status}");
            
            Thread.Sleep(1000);
            Console.WriteLine($"Task status: {task.Status}");
        }

        private static Task MyTask()
        {
            return new Task(() => Console.WriteLine("MyTask"));
        }

        private static string MyReturnTask()
        {
            return "MyReturnTask";
        }

        private static void StartAsyncExample()
        {
            AsyncExample asyncExample = new AsyncExample("ten_thousand");
            Task<int> task = asyncExample.GiveFirstElementAsync();
            Console.WriteLine($"Task<int> >> {task}");
            Console.WriteLine($"Task Result >> {task.Result}");
        }

        private static void StartLockExample()
        {
            Thread t1 = new Thread(() =>
            {
                lock (Locker)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        SharedResource++;
                    }
                }
            });

            Thread t2 = new Thread(() =>
            {
                lock (Locker)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        SharedResource--;
                    }
                }
            });

            t1.Start();
            t2.Start();

            Thread.Sleep(2000);
            Console.WriteLine(SharedResource);
        }
    }
}
