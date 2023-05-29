﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace POOWorkersAdminV1
{
    public class AppController
    {
        private WorkerManager workerManager;
        private TeamManager teamManager;
        private TaskManager taskManager;
        private bool exit;
        private Dictionary<WorkerRoles, string[]> authorizedOptions;
        private Dictionary<string, string> optionNames;

        int numberOfAttempts = 0;
        int maxNumberOfAttempts;

        public WorkerRoles? userRole { get; set; }
        public Team UserTeam { get; set; }
        public ItWorker ActiveUser { get; set; }

        public AppController(TaskManager taskManager, WorkerManager workerManager, TeamManager teamManager)
        {
            this.teamManager = teamManager;
            this.taskManager = taskManager;
            this.workerManager = workerManager;
            exit = false;
            maxNumberOfAttempts = 3;
            authorizedOptions = new Dictionary<WorkerRoles, string[]>(){
                { WorkerRoles.Admin, new string[]{"1","2","3","4","5","6","7","8","9","10","11","12"} },
                { WorkerRoles.Manager, new string[]{"5","6","7","9","10","12"} },
                { WorkerRoles.Worker, new string[]{"6","7","10","12"} },
            };
            optionNames = new Dictionary<string, string>()
            {
                {"1", "Register new IT worker"},
                {"2", "Register new team"},
                {"3", "Register new task (unassigned to anyone)"},
                {"4", "List all team names"},
                {"5", "List team members by team name"},
                {"6", "List unassigned tasks"},
                {"7", "List tasks assignments by team name"},
                {"8", "Assign IT worker to a team as manager"},
                {"9", "Assign IT worker to a team as technician"},
                {"10", "Assign task to IT worker"},
                {"11", "Unregister worker"},
                {"12", "Exit"},
            };

            WorkerRoles? userRole = null;
            Team userTeam = null;
            ItWorker activeUser = null;
        }

        public void Run()
        {

            LoginUser();

            if (exit) { return; }

            do
            {
                PrintMenus();
                AskForOption();
            }
            while (!exit);
 
        }

        public void LoginUser()
        {

            while (userRole is null)
            {
                Console.WriteLine("Wellcome to your Bank");
                int? userId = AskForInteger("Introduce your user id:", 0);

                if (userId == 0)
                {
                    userRole = WorkerRoles.Admin;
                    break;
                }

                if (userId > 0)
                {
                    ItWorker worker = workerManager.GetWorkerById((int)userId);
                    if (worker != null)
                    {
                        ActiveUser = worker;
                        UserTeam = teamManager.GetTeamByWorkerId(worker.Id);
                        if (UserTeam.Manager.Id == worker.Id)
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
        }

        public bool CheckUserAuth(string chosenOption)
        {
            return authorizedOptions[(WorkerRoles)userRole].Contains(chosenOption);
        }

        public void AskForOption()
        {
            string chosenOption = AskForString("Introduce an option");

            if (!CheckUserAuth(chosenOption))
            {
                Console.WriteLine("Not authorised");
                return;
            }

            switch (chosenOption)
            {
                case "1":
                    RegisterNewItWorker();
                    break;

                case "2":
                    RegisterNewTeam();
                    break;

                case "3":
                    RegisterNewTask();
                    break;

                case "4":
                    ListTeamNames();
                    break;

                case "5":
                    ListTeamMembersByTeamName();
                    break;

                case "6":
                    ListUnassignedTasks();
                    break;

                case "7":
                    ListTasksAssignmentsByTeamName();
                    break;

                case "8":
                    AssignTeamManager();
                    break;

                case "9":
                    AssignTeamTechnician();
                    break;

                case "10":
                    AssignTaskToWorker();
                    break;

                case "11":
                    UnregisterWorker();
                    break;

                case "12":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Option not available. Please introduce a number between 1 and 12");
                    break;
            }

        }

        public void PrintMenus()
        {
            Console.WriteLine("==========OPTIONS===========");

            var keys = authorizedOptions[(WorkerRoles)userRole];
            foreach (string key in keys)
            {
                Console.WriteLine($"{key}. {optionNames[key]}");
            }

            //switch (userRole)
            //{
            //    case WorkerRoles.Admin:
            //        Console.WriteLine("=====================");
            //        Console.WriteLine("1. Register new IT worker");
            //        Console.WriteLine("2. Register new team");
            //        Console.WriteLine("3. Register new task (unassigned to anyone)");
            //        Console.WriteLine("4. List all team names");
            //        Console.WriteLine("5. List team members by team name");
            //        Console.WriteLine("6. List unassigned tasks");
            //        Console.WriteLine("7. List tasks assignments by team name");
            //        Console.WriteLine("8. Assign IT worker to a team as manager");
            //        Console.WriteLine("9. Assign IT worker to a team as technician");
            //        Console.WriteLine("10. Assign task to IT worker");
            //        Console.WriteLine("11. Unregister worker");
            //        Console.WriteLine("12. Exit");
            //        break;
            //    case WorkerRoles.Manager:
            //        Console.WriteLine("5. List team members by team name");
            //        Console.WriteLine("6. List unassigned tasks");
            //        Console.WriteLine("7. List tasks assignments by team name");
            //        Console.WriteLine("9. Assign IT worker to a team as technician");
            //        Console.WriteLine("10. Assign task to IT worker");
            //        Console.WriteLine("12. Exit");
            //        break;
            //    case WorkerRoles.Worker:
            //        Console.WriteLine("=====================");
            //        Console.WriteLine("6. List unassigned tasks");
            //        Console.WriteLine("7. List tasks assignments by team name");
            //        Console.WriteLine("10. Assign task to IT worker");
            //        Console.WriteLine("12. Exit");
            //        break;
            //    default:
            //        break;
            //}
        }

        public void RegisterNewItWorker()
        {
            Console.WriteLine("Introduce the following data in order to create a new It worker:");
            string workerName = AskForString("Name:");
            if (workerName == null) { return; }

            string workerSurname = AskForString("Surname:");
            if (workerSurname == null) { return; }

            var workerDateOfBirth = AskForDate("Date of birth:");
            if (workerDateOfBirth == null) { return; }

            var workerYearsOfExperience = AskForUnsignedInteger("Years of experience:");
            if (workerYearsOfExperience == null) { return; }

            string rawInput = AskForString("Technologies known (write them separated by a coma):");
            if (rawInput == null) { return; }
            List<string> workerTechnologies = rawInput.Split(',').Select(e => e.ToLower()).ToList();
            //var workerTechnologies = rawInput.Split(',').ToList();

            var workerLevel = AskForWorkerLevel("Introduce the worker's level (Junior, Medium, Senior)");
            if (workerLevel == null) { return; }

            var newWorker = new ItWorker(workerName, workerSurname, (DateTime)workerDateOfBirth, (int)workerYearsOfExperience, workerTechnologies, (WorkerLevel)workerLevel);

            if (!workerManager.RegisterNewWorker(newWorker))
            {
                Console.WriteLine("User unsuccesfully registered");
            }
            Console.WriteLine("User registered successfully");
        }

        public void RegisterNewTeam()
        {
            Console.WriteLine("Introduce the following data in order to create a new team:");
            string newTeamName = AskForString("Name:");
            if (newTeamName == null) { return; }

            var newTeamManagerId = AskForUnsignedInteger("Introduce the id of the worker to become manager");
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

        public void RegisterNewTask()
        {
            Console.WriteLine("Introduce the following data in order to create a new task:");
            string taskName = AskForString("Name:");
            if (taskName == null) { return; }

            string taskDescription = AskForString("Description:");
            if (taskDescription == null) { return; }

            string taskTechnology = AskForString("Technology:");
            if (taskTechnology == null) { return; }

            var newTask = new Task(taskName, taskDescription, taskTechnology);
            if (!taskManager.RegisterNewTask(newTask))
            {
                Console.WriteLine("Task NOT registered successfully!");
            }
            Console.WriteLine("Task registered successfully!");
        }

        public void ListTeamNames()
        {
            Console.WriteLine("The team names are:");
            var allTeams = teamManager.Teams.ToList();
            foreach (var team in allTeams)
            {
                Console.Write(team.Name);
            }
        }

        public void ListTeamMembersByTeamName()
        {
            string teamName;

            if (userRole == WorkerRoles.Manager)
            {
                teamName = UserTeam.Name;
            }
            else
            {
                teamName = AskForString("Introduce the Team's name:");
                if (teamName == null) { return; }
            }

            Team team = teamManager.GetTeamByName(teamName);
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

        public void ListUnassignedTasks()
        {
            Console.WriteLine("Unassigned tasks:");
            var unassignedTasks = taskManager.GetTasksByIdWorker(null);
            foreach (Task task in unassignedTasks)
            {
                Console.WriteLine(task.Name);
            }
        }

        public void ListTasksAssignmentsByTeamName()
        {
            Team team;

            if (userRole == WorkerRoles.Manager || userRole == WorkerRoles.Worker)
            {
                team = UserTeam;
            }
            else
            {
                string teamName = AskForString("Introduce the team's name");
                if (teamName == null) { return; }
                team = teamManager.GetTeamByName(teamName);
            }

            if (team == null)
            {
                Console.WriteLine("No team found with such a name");
                return;
            }

            var assignedTasks = new List<Task>();
            foreach (ItWorker worker in team.Technicians)
            {
                // TODO - check AddRange method
                assignedTasks.AddRange(taskManager.GetTasksByIdWorker(worker.Id));
            }
            assignedTasks.AddRange(taskManager.GetTasksByIdWorker(team.Manager.Id));

            Console.WriteLine("Tasks assigned to team:");
            foreach (Task task in assignedTasks)
            {
                Console.WriteLine(task.ToString());
            }
        }

        public void AssignTeamManager()
        {
            int? workerId = AskForUnsignedInteger("Introduce the worker's id");
            if (workerId == null) { return; }

            ItWorker worker = workerManager.GetWorkerById((int)workerId);
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

            int? teamId = AskForUnsignedInteger("Introduce the team's id");
            if (teamId == null) { return; }

            Team team = teamManager.GetTeamById((int)teamId);
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

        public void AssignTeamTechnician()
        {
            int? workerId = AskForUnsignedInteger("Introduce the worker's id");
            if (workerId == null) { return; }

            ItWorker worker = workerManager.GetWorkerById((int)workerId);
            if (worker == null)
            {
                Console.WriteLine("No IT worker found with such an ID");
                return;
            }

            int? teamId = AskForUnsignedInteger("Introduce the team's id");
            if (teamId == null) { return; }

            Team team = teamManager.GetTeamById((int)teamId);
            if (team == null)
            {
                Console.WriteLine("No team found with such an ID");
                return;
            }

            if (!team.AddTechnician(worker))
            {
                Console.WriteLine("Technician was NOT added because already exists");
            }

            Console.WriteLine("Technician added correctly");
        }

        public void AssignTaskToWorker()
        {
            Console.WriteLine("Introduce the following data in order to assign a task");
            int? idWorker;

            if (userRole != WorkerRoles.Worker)
            {
                idWorker = AskForUnsignedInteger("Introduce the worker's id");
                if (idWorker == null) { return; }
            }
            else
            {
                idWorker = ActiveUser.Id;
            }

            ItWorker worker = workerManager.GetWorkerById((int)idWorker);
            if (worker == null)
            {
                Console.WriteLine("No worker found with such an id");
                return;
            }

            string taskName = AskForString("Task's name:");
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

        public void UnregisterWorker()
        {
            Console.WriteLine("Introduce the following data in order to unregister a worker");
            var idWorker = AskForUnsignedInteger("Introduce the worker's id");
            if (idWorker == null) { return; }

            ItWorker worker = workerManager.GetWorkerById((int)idWorker);
            if (worker == null)
            {
                Console.WriteLine("No worker found with such an id");
                return;
            }

            if (!workerManager.UnregisterWorkerById((int)idWorker) 
                || !taskManager.DeleteIdWorkerFromTasks((int)idWorker)
                || !teamManager.DeleteIdWorkerFromTeam((int)idWorker))
            {
                Console.WriteLine("Worker NOT unregistered correctly");
            }
            Console.WriteLine("Worker unregistered succesfully");
        }

        public int? AskForUnsignedInteger(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be a positive integer.");
            numberOfAttempts = 0;

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
            exit = true;
            return null;
        }

        public int? AskForInteger(string consoleText, int minimumValue)
        {
            Console.WriteLine($"{consoleText}. It must be an integer greater or equal to {minimumValue}.");
            numberOfAttempts = 0;

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (int.TryParse(Console.ReadLine(), out int validatedInput) && validatedInput >= minimumValue)
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
            exit = true;
            return null;
        }

        public decimal? AskForUnsignedDecimal(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be a valid number.");
            numberOfAttempts = 0;

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
            exit = true;
            return null;
        }

        public DateTime? AskForDate(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be dd/MM/yyyy");
            numberOfAttempts = 0;

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input follows the correct date format");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }

            Console.WriteLine("Too many attempts, try again later");
            exit = true;
            return null;
        }

        public WorkerLevel? AskForWorkerLevel(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be Senior, Medium or Junior");
            numberOfAttempts = 0;

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (WorkerLevel.TryParse(Console.ReadLine(), out WorkerLevel validatedInput))
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
            exit = true;
            return null;
        }

        public TaskStatus? AskForTaskStatus(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be ToDo, Doing or Done");
            numberOfAttempts = 0;

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (WorkerLevel.TryParse(Console.ReadLine(), out TaskStatus validatedInput))
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
            exit = true;
            return null;
        }

        public string AskForString(string consoleText)
        {
            Console.WriteLine($"{consoleText}. It must be a valid string");
            numberOfAttempts = 0;

            while (numberOfAttempts < maxNumberOfAttempts)
            {
                var userInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is not an empty text");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }

            Console.WriteLine("Too many attempts, try again later");
            exit = true;
            return null;
        }
    }
}
