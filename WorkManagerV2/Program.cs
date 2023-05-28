using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POOWorkersAdminV1;

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

            var workerManager = new WorkerManager(workers);
            var teamManager = new TeamManager();
            var taskManager = new TaskManager();

            WorkerRoles? userRole = null;
            Team userTeam = null;
            ItWorker activeUser = null;

            while (userRole is null)
            {
                Console.WriteLine("Wellcome to your Bank");
                Console.WriteLine("Introduce your user id:");
                var userId = DataValidator.AskForUnsignedInteger();

                if (userId == 0)
                {
                    userRole = WorkerRoles.Admin;
                    break;
                }

                if (userId > 0)
                {
                    var worker = workerManager.GetWorkerById((int)userId);
                    if (worker != null)
                    {
                        activeUser = worker;
                        userTeam = teamManager.GetTeamByWorkerId(worker.Id);
                        if (userTeam.Manager.Id == worker.Id)
                        {
                            userRole = WorkerRoles.Manager;
                            break;
                        }
                        else
                        {
                            userRole = WorkerRoles.Worker;
                            break;
                        }
                    }
                }
            }
 
            var exit = false;
            do
            {
                switch(userRole)
                {
                    case WorkerRoles.Admin:
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
                        break;
                    case WorkerRoles.Manager:
                        Console.WriteLine("=====================");
                        Console.WriteLine("Introduce an option");
                        Console.WriteLine("5. List team members by team name");
                        Console.WriteLine("6. List unassigned tasks");
                        Console.WriteLine("7. List tasks assignments by team name");
                        Console.WriteLine("9. Assign IT worker to a team as technician");
                        Console.WriteLine("10. Assign task to IT worker");
                        Console.WriteLine("12. Exit");
                        break;
                    case WorkerRoles.Worker:
                        Console.WriteLine("=====================");
                        Console.WriteLine("Introduce an option");
                        Console.WriteLine("6. List unassigned tasks");
                        Console.WriteLine("7. List tasks assignments by team name");
                        Console.WriteLine("10. Assign task to IT worker");
                        Console.WriteLine("12. Exit");
                        break;
                    default:
                        break;
                }

                switch (Console.ReadLine())
                {
                    case "1":
                        if (userRole == WorkerRoles.Worker || userRole == WorkerRoles.Manager)
                        {
                            Console.WriteLine("Not allowed");
                            break;
                        }
                        AppController.RegisterNewItWorker(workerManager);
                        break;

                    case "2":
                        if (userRole == WorkerRoles.Worker || userRole == WorkerRoles.Manager)
                        {
                            Console.WriteLine("Not allowed");
                            break;
                        }
                        AppController.RegisterNewTeam(workerManager);
                        break;

                    case "3":
                        if (userRole == WorkerRoles.Worker || userRole == WorkerRoles.Manager)
                        {
                            Console.WriteLine("Not allowed");
                            break;
                        }
                        AppController.RegisterNewTask(taskManager);
                        break;

                    case "4":
                        if (userRole == WorkerRoles.Worker || userRole == WorkerRoles.Manager)
                        {
                            Console.WriteLine("Not allowed");
                            break;
                        }
                        AppController.ListTeamNames(teamManager);
                        break;

                    case "5":
                        if (userRole == WorkerRoles.Worker)
                        {
                            Console.WriteLine("Not allowed");
                        }
                        else if (userRole == WorkerRoles.Manager)
                        {
                            Console.WriteLine("Technicians in team:");
                            foreach (var technician in userTeam.Technicians)
                            {
                                Console.WriteLine(technician.ToString());
                            }
                        }
                        else
                        {
                            AppController.ListTeamMembersByTeamName(teamManager);
                        }
                        break;

                    case "6":
                        AppController.ListUnassignedTasks(taskManager);
                        break;

                    case "7":
                        if (userRole == WorkerRoles.Worker || userRole == WorkerRoles.Manager)
                        {
                            var assignedTasks = new List<Task>();
                            Console.WriteLine("Tasks for team:");
                            foreach (var technician in userTeam.Technicians)
                            {
                                assignedTasks.AddRange(taskManager.GetTasksByIdWorker(technician.Id));
                            }
                            assignedTasks.AddRange(taskManager.GetTasksByIdWorker(userTeam.Manager.Id));

                            Console.WriteLine("Tasks assigned to team:");
                            foreach (var task in assignedTasks)
                            {
                                Console.WriteLine(task.ToString());
                            }
                        }
                        else
                        {
                            AppController.ListTasksAssignmentsByTeamName(teamManager, taskManager);
                        }
                        break;

                    case "8":
                        if (userRole == WorkerRoles.Worker || userRole == WorkerRoles.Manager)
                        {
                            Console.WriteLine("Not allowed");
                        }
                        else
                        {
                            AppController.AssignTeamManager(teamManager, workerManager);
                        }
                        break;

                    case "9":
                        if (userRole == WorkerRoles.Worker)
                        {
                            Console.WriteLine("Not allowed");
                            break;
                        }
                        AppController.AssignTeamTechnician(teamManager, workerManager);
                        break;

                    case "10":
                        if (userRole == WorkerRoles.Worker)
                        {
                            Console.WriteLine("Introduce the following data in order to assign a task to a yourself:");

                            Console.WriteLine("Task's name:");
                            var taskName = Console.ReadLine();
                            if (taskName == null) { return; }
                            var task = taskManager.GetTaskByName(taskName);
                            if (task == null)
                            {
                                Console.WriteLine("No task found with such a name");
                                return;
                            }

                            if (activeUser.TechKnowleges.Contains(task.Technology)
                                && taskManager.AssignTaskToWorker(activeUser.Id, task.Id))
                            {
                                Console.WriteLine("Task assigned correctly");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("You do not have the technologies required or task is already done");
                                Console.WriteLine("Task was not assigned");
                            }

                            break;
                        }
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
