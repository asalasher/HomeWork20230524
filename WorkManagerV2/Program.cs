using POOWorkersAdminV1;
using System;
using System.Collections.Generic;

namespace WorkManagerV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Mock data
            List<ItWorker> workers = new List<ItWorker>()
            {
                new ItWorker("Pedro", "Liarte",new DateTime(2010, 6, 1), 2, new List<string>(){"mySql", "javascript"}, WorkerLevel.Junior),
                new ItWorker("Maria", "Vela", new DateTime(2000, 6, 1), 5, new List<string>(){"golang", "c++"}, WorkerLevel.Junior),
                new ItWorker("Adrian", "Alquezar", new DateTime(1990, 6, 1), 1, new List<string>(){"c", "c#"}, WorkerLevel.Medium),
                new ItWorker("Alberto", "Salas", new DateTime(1989, 6, 1), 10, new List<string>(){"c", "c#"}, WorkerLevel.Senior),
            };

            var appController = new AppController(new TaskManager(), new WorkerManager(workers), new TeamManager());
            appController.Run();

            Console.WriteLine("Closing the app...");
            Console.Write("Press a key to continue");
            Console.ReadKey();

        }
    }
}
