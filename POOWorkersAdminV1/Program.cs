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
                        RegisterNewItWorker(workerManager);
                        break;
                    case "2":
                        RegisterNewTeam(workerManager);
                        break;
                    case "3":
                        RegisterNewTask(taskManager);
                        break;
                    case "4":
                        ListTeamNames(teamManager);
                        break;
                    case "5":
                        ListTeamMembersByTeamName(teamManager);
                        break;
                    case "6":
                        ListUnassignedTasks(taskManager);
                        break;
                    case "7":
                        ListTasksAssignmentsByTeamName(teamManager, taskManager);
                        break;
                    case "8":
                        AssignTeamManager(teamManager, workerManager);
                        break;
                    case "9":
                        AssignTeamTechnician(teamManager, workerManager);
                        break;
                    case "10":
                        AssignTaskToWorker(taskManager, workerManager);
                        break;
                    case "11":
                        UnregisterWorker(taskManager, workerManager, teamManager);
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

        public static void RegisterNewItWorker(WorkerManager workerManger)
        {
            Console.WriteLine("Introduce the following data in order to create a new It worker:");

            Console.WriteLine("Name:");
            var workerName = Console.ReadLine();

            Console.WriteLine("Surname:");
            var workerSurname = Console.ReadLine();

            Console.WriteLine("Date of birth:");
            var workerDateOfBirth = DataValidator.AskForDate();
            if (workerDateOfBirth == null) { return; }

            Console.WriteLine("Years of experience:");
            var workerYearsOfExperience = DataValidator.AskForUnsignedInteger();
            if (workerYearsOfExperience == null) { return; }

            Console.WriteLine("Technologies known (write them separated by a coma):");
            var rawInput = Console.ReadLine();
            var workerTechnologies = rawInput.Split(',').Select(e => e.ToLower()).ToList();
            //var workerTechnologies = rawInput.Split(',').ToList();

            Console.WriteLine("Level:");
            Console.WriteLine("Introduce the worker's level (Junior, Medium, Senior)");
            var workerLevel = DataValidator.AskForWorkerLevel();
            if (workerLevel == null) { return; }

            var newWorker = new ItWorker(workerName, workerSurname, (DateTime)workerDateOfBirth, (int)workerYearsOfExperience, workerTechnologies, (WorkerLevel)workerLevel);

            if (workerManger.RegisterNewWorker(newWorker))
            {
                Console.WriteLine("User registered correctly");
            }
            else
            {
                Console.WriteLine("User unsuccesfully registered");
            }
        }

        public static void RegisterNewTeam(WorkerManager workerManager)
        {
            Console.WriteLine("Introduce the following data in order to create a new team:");

            Console.WriteLine("Name:");
            var newTeamName = Console.ReadLine();

            Console.WriteLine("Id of IT worker that will be the manager (it has to be an unsigned integer):");
            var newTeamManagerId = DataValidator.AskForUnsignedInteger();
            if (newTeamManagerId == null) { return; }
            var newTeamManager = workerManager.GetWorkerById((int)newTeamManagerId);

            try
            {
                Team newTeam = new Team(newTeamManager, newTeamName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("The worker must be senior in order to be eligible for a manager position");
            }
        }

        public static void RegisterNewTask(TaskManager taskManager)
        {
            Console.WriteLine("Introduce the following data in order to create a new task:");

            // TODO - check for null or empty strings
            Console.WriteLine("Name:");
            var taskName = Console.ReadLine();

            Console.WriteLine("Description:");
            var taskDescription = Console.ReadLine();

            Console.WriteLine("Technology:");
            var taskTechnology = Console.ReadLine();

            var newTask = new Task(taskName, taskDescription, taskTechnology);
            if (taskManager.RegisterNewTask(newTask))
            {
                Console.WriteLine("Task registered correctly!");
            }
        }

        public static void ListTeamNames(TeamManager teamManager)
        {
            Console.WriteLine("The team names are:");

            var allTeams = teamManager.Teams.ToList();
            foreach (var team in allTeams)
            {
                Console.Write(team.Name);
            }
        }

        public static void ListTeamMembersByTeamName(TeamManager teamManager)
        {
            Console.WriteLine("Introduce the Team's name:");
            var teamName = Console.ReadLine();

            var team = teamManager.GetTeamByName(teamName);
            if (team == null)
            {
                Console.WriteLine("No team found with such a name");
                return;
            }

            Console.WriteLine("List of technicians:");
            foreach (var worker in team.Technicians)
            {
                Console.WriteLine(worker.ToString());
            }

            Console.WriteLine("Manager:");
            Console.WriteLine(team.Manager.ToString());
        }

        public static void ListUnassignedTasks(TaskManager taskManager)
        {
            Console.WriteLine("Unassigned tasks:");
            var unassignedTasks = taskManager.GetTasksByIdWorker(null);
            foreach (var task in unassignedTasks)
            {
                Console.WriteLine(task.Name);
            }
        }

        public static void ListTasksAssignmentsByTeamName(TeamManager teamManager, TaskManager taskManager)
        {
            Console.WriteLine("Introduce the following data to get the task's assignments");
            Console.WriteLine("Introduce the team's name");
            var teamName = Console.ReadLine();
            
            var team = teamManager.GetTeamByName(teamName);

            if (team == null)
            {
                Console.WriteLine("No team found with such a name");
                return;
            }

            var assignedTasks = new List<Task>();

            foreach (var worker in team.Technicians)
            {
                // TODO - check AdddRange method
                assignedTasks.AddRange(taskManager.GetTasksByIdWorker(worker.Id));
            }
            assignedTasks.AddRange(taskManager.GetTasksByIdWorker(team.Manager.Id));

            Console.WriteLine("Tasks assigned to team:");
            foreach (var task in assignedTasks)
            {
                Console.WriteLine(task.ToString());
            }
        }

        public static void AssignTeamManager(TeamManager teamManager, WorkerManager workerManager)
        {
            Console.WriteLine("Introduce the following data in order to assign a worker to a team as a manager");

            Console.WriteLine("Worker's id:");
            var workerId = DataValidator.AskForUnsignedInteger();
            if (workerId == null) { return; }
            var worker = workerManager.GetWorkerById((int)workerId);
            if (worker == null)
            {
                Console.WriteLine("No IT worker found with such an ID");
                return;
            }

            if (worker.Level != WorkerLevel.Senior)
            {
                Console.WriteLine("The worker must be senior to be a manager");
                 return;
            }

            Console.WriteLine("Team's id:");
            var teamId = DataValidator.AskForUnsignedInteger();
            if (teamId == null) { return; }
            var team = teamManager.GetTeamById((int)teamId);
            if (team == null)
            {
                Console.WriteLine("No team found with such an ID");
                return;
            }
            if (team.Manager != null)
            {
                Console.WriteLine("The team already has a manager");
                return;
            }
        }

        public static void AssignTeamTechnician(TeamManager teamManager, WorkerManager workerManager)
        {
            Console.WriteLine("Introduce the following data in order to assign a worker to a team as a technician");

            Console.WriteLine("Worker's id:");
            var workerId = DataValidator.AskForUnsignedInteger();
            if (workerId == null) { return; }
            var worker = workerManager.GetWorkerById((int)workerId);
            if (worker == null)
            {
                Console.WriteLine("No IT worker found with such an ID");
                return;
            }

            Console.WriteLine("Team's id:");
            var teamId = DataValidator.AskForUnsignedInteger();
            if (teamId == null) { return; }
            var team = teamManager.GetTeamById((int)teamId);
            if (team == null)
            {
                Console.WriteLine("No team found with such an ID");
                return;
            }

            if (team.AddTechnician(worker))
            {
                Console.WriteLine("Technician added correctly");
            }
            else
            {
                Console.WriteLine("Technician was NOT added because already exists");
            }
        }

        public static void AssignTaskToWorker(TaskManager taskManager, WorkerManager workerManager)
        {
            Console.WriteLine("Introduce the following data in order to assign a task to a worker");

            Console.WriteLine("Worker's id:");
            var idWorker = DataValidator.AskForUnsignedInteger();
            if (idWorker == null) { return; }
            var worker = workerManager.GetWorkerById((int)idWorker);
            if (worker == null)
            {
                Console.WriteLine("No worker found with such an id");
                return;
            }

            Console.WriteLine("Task's name:");
            var taskName = Console.ReadLine();
            if (taskName == null) { return; }
            var task = taskManager.GetTaskByName(taskName);
            if (task == null)
            {
                Console.WriteLine("No task found with such a name");
                return;
            }

            if (worker.TechKnowleges.Contains(task.Technology)
                && taskManager.AssignTaskToWorker((int)idWorker, task.Id))
            {
                Console.WriteLine("Task assigned correctly");
                return;
            }

            Console.WriteLine("Worker does not know the technologies required");
            Console.WriteLine("Task was not assigned");
        }

        public static void UnregisterWorker(TaskManager taskManager, WorkerManager workerManager, TeamManager teamManager)
        {
            Console.WriteLine("Introduce the following data in order to unregister a worker");

            Console.WriteLine("Worker's id:");
            var idWorker = DataValidator.AskForUnsignedInteger();
            if (idWorker == null) { return; }
            var worker = workerManager.GetWorkerById((int)idWorker);
            if (worker == null)
            {
                Console.WriteLine("No worker found with such an id");
                return;
            }

            if (!workerManager.UnregisterWorkerById((int)idWorker))
            {
                Console.WriteLine("Worker NOT unregistered correctly");
            }

            if (!taskManager.DeleteIdWorkerFromTasks((int)idWorker))
            {
                Console.WriteLine("Worker NOT unregistered correctly");
            }


            if (!teamManager.DeleteIdWorkerFromTeam((int)idWorker))
            {
                Console.WriteLine("Worker NOT unregistered correctly");
            }

            Console.WriteLine("Worker unregistered succesfully");
        }

    }
}
