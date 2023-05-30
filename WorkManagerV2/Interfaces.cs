using POOWorkersAdminV1;
using System.Collections.Generic;

namespace WorkManagerV2
{

    public interface ITaskManager
    {
        bool RegisterNewTask(Task newtask);
        List<Task> GetTasksByIdWorker(int? idWorker);
        bool AssignTaskToWorker(int idWorker, int idTask);
        Task GetTaskByName(string taskName);
        bool DeleteIdWorkerFromTasks(int idWorker);
    }

    public interface IWorkerManager
    {
        ItWorker GetWorkerById(int id);
        bool RegisterNewWorker(ItWorker worker);
        bool UnregisterWorkerById(int idWorker);
    }

    public interface ITeamManager
    {
        List<Team> Teams { get; set; }
        bool RegisterNewTeam(Team team);
        Team GetTeamByName(string name);
        Team GetTeamById(int id);
        Team GetTeamByWorkerId(int idWorker);
        bool DeleteIdWorkerFromTeam(int idWorker);
    }

}
