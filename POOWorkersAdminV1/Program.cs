using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
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

            var workerManager = new WorkerManager(workers);
            var teamManager = new TeamManager();
            var taskManager = new TaskManager();

            Console.WriteLine("Wellcome to the ERP");

            var exit = false;
            do
            {
                Console.WriteLine("=====================");
                Console.WriteLine("Introduce an option");
                Console.WriteLine("1. Register new IT worker");
                Console.WriteLine("2. Register new team");
                Console.WriteLine("3. Register new task (unassigned to anyone)");
                Console.WriteLine("4. List all team names");
                Console.WriteLine("5. List team members by team name");
                Console.WriteLine("6. List unassigned tasks");
                Console.WriteLine("7. List tasks assignments by team name");
                Console.WriteLine("8. Assign IT worker to a team as manager");
                Console.WriteLine("9. Assign IT worker to a team as technician");
                Console.WriteLine("10. Assign task to IT worker");
                Console.WriteLine("11. Unregister worker");
                Console.WriteLine("12. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        AppController.RegisterNewItWorker(workerManager);
                        break;
                    case "2":
                        AppController.RegisterNewTeam(workerManager);
                        break;
                    case "3":
                        AppController.RegisterNewTask(taskManager);
                        break;
                    case "4":
                        AppController.ListTeamNames(teamManager);
                        break;
                    case "5":
                        AppController.ListTeamMembersByTeamName(teamManager);
                        break;
                    case "6":
                        AppController.ListUnassignedTasks(taskManager);
                        break;
                    case "7":
                        AppController.ListTasksAssignmentsByTeamName(teamManager, taskManager);
                        break;
                    case "8":
                        AppController.AssignTeamManager(teamManager, workerManager);
                        break;
                    case "9":
                        AppController.AssignTeamTechnician(teamManager, workerManager);
                        break;
                    case "10":
                        AppController.AssignTaskToWorker(taskManager, workerManager);
                        break;
                    case "11":
                        AppController.UnregisterWorker(taskManager, workerManager, teamManager);
                        break;
                    case "12":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Option not available. Please introduce a number between 1 and 12");
                        break;
                }
            } while (!exit);
            Console.WriteLine("Closing the app...");
            Console.WriteLine("Press a key to continue");
            Console.ReadLine();
        }
    }
}
