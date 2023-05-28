using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
{
    public class AppController
    {

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
                // TODO - check AddRange method
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

            Console.WriteLine("Worker does not know the technologies required or task is already done");
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
