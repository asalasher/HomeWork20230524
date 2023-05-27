using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // una vez finalizada cada operacion el programa preguntará al usario si quiere realizar alguna otra operación
            // si el usuario dice que no, mostrará el valor actual de saldo y finalizará el programa

            // Mock data
            List<ItWorker> workers = new List<ItWorker>()
            {
                new ItWorker("Pedro", "Liarte", new DateTime(1989, 09, 16), 2, new List<string>(){"mySql", "javascript"}, WorkerLevel.Junior),
                new ItWorker("Maria", "Vela", new DateTime(1990, 02, 12), 5, new List<string>(){"golang", "c++"}, WorkerLevel.Junior),
                new ItWorker("Adrian", "Alquezar", new DateTime(1991, 12, 11), 1, new List<string>(){"c", "c#"}, WorkerLevel.Medium),
                new ItWorker("Perico", "Rodriguez", new DateTime(1981, 17, 01), 10, new List<string>(){"c", "c#"}, WorkerLevel.Senior),
            };

            var workerManager = new WorkerManager(workers);
            var teamManager = new TeamManager();
            var taskManager = new TaskManager();

            Console.WriteLine("Wellcome to the ERP");

            var exit = false;
            do
            {
                // Options
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

                //TODO - change methods
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
                        AssignTaskToWorker();
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

        public static int? AskForUnsignedInteger()
        {
            var numberOfAttempts = 0;
            var maxNumberOfAttempts = 3;
            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (int.TryParse(Console.ReadLine(), out int validatedInput) && validatedInput > 0)
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a positive integer value");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            return null;
        }

        public static decimal? AskForUnsignedDecimal()
        {
            var numberOfAttempts = 0;
            var maxNumberOfAttempts = 3;
            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal validatedInput) && validatedInput > 0)
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a positive decimal value");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }

            Console.WriteLine("Too many attempts, try again later");
            return null;
        }

        public static void RegisterNewItWorker(WorkerManager workerManger)
        {
            Console.WriteLine("Introduce the following data in order to create a new It worker:");

            Console.WriteLine("Name:");
            var workerName = Console.ReadLine();

            Console.WriteLine("Surname:");
            var workerSurname = Console.ReadLine();

            Console.WriteLine("Date of birth:"); // TODO
            DateTime? workerDateOfBirth = null;
            if (workerDateOfBirth == null) { return; }

            Console.WriteLine("Years of experience:");
            var workerYearsOfExperience = AskForUnsignedInteger();
            if (workerYearsOfExperience == null) { return; }

            // TODO - split by ,
            Console.WriteLine("Technologies knows (write them separated by a coma):");

            Console.WriteLine("Level");

            var newWorker = new ItWorker(workerName, workerSurname, workerDateOfBirth, workerYearsOfExperience, workerTechnologies, workerLevel);

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
            var newTeamManagerId = AskForUnsignedInteger();
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

            var newTask = new Task(taskName, taskDescription);
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
            var unassignedTasks = taskManager.GetTaskByIdWorker(null);
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
            var workerId = AskForUnsignedInteger();
            if (workerId == null) { return; }
            var worker = workerManager.GetWorkerById((int)workerId);
            if (worker == null)
            {
                Console.WriteLine("No IT worker found with such an ID");
                return;
            }

            Console.WriteLine("Team's id:");
            var teamId = AskForUnsignedInteger();
            if (teamId == null) { return; }
            var team = teamManager.GetTeamById((int)teamId);
            if (team == null)
            {
                Console.WriteLine("No team found with such an ID");
                return;
            }
        }

        public static void AssignTeamTechnician(TeamManager teamManager, WorkerManager workerManager)
        {
            Console.WriteLine("Introduce the following data in order to assign a worker to a team as a technician");

            Console.WriteLine("Worker's id:");
            var workerId = AskForUnsignedInteger();
            if (workerId == null) { return; }
            var worker = workerManager.GetWorkerById((int)workerId);
            if (worker == null)
            {
                Console.WriteLine("No IT worker found with such an ID");
                return;
            }

            Console.WriteLine("Team's id:");
            var teamId = AskForUnsignedInteger();
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

        public static void AssignTaskToWorker()
        {
            Console.WriteLine("Introduce the following data in order to assign a task to a worker");

            Console.WriteLine("Worker's id:");
            var workerId = AskForUnsignedInteger();
            if (workerId == null) { return; }

            Console.WriteLine("Task's id:");
            var taskId = AskForUnsignedInteger();
            if (taskId == null) { return; }
        }

        public static void UnregisterWorker(TaskManager taskManager, WorkerManager workerManager, TeamManager teamManager)
        {
            // TODO - remove user
            // TODO - remove IdWorker from tasks
            // TODO - remove IdWorker from team, checking wether it is a manager or not

        }
    }
}
