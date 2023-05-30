using System;
using System.Collections.Generic;
using WorkManagerV2;

namespace POOWorkersAdminV1
{
    public class TaskManager: ITaskManager
    {

        private List<Task> tasks;
        public TaskManager()
        {
            tasks = new List<Task>();
        }

        public bool RegisterNewTask(Task newTask)
        {
            tasks.Add(newTask);
            return true;
        }

        public List<Task> GetTasksByIdWorker(int? idWorker)
        {
            var tasks = new List<Task>();

            foreach (var task in tasks)
            {
                if (task.IdWorker == idWorker)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }
        public bool AssignTaskToWorker(int idWorker, int idTask)
        {
            foreach (var task in tasks)
            {
                if (task.Id == idTask && task.Status != TaskStatus.Done )
                {
                    task.IdWorker = idWorker;
                    return true;
                }
            }

            return false;
        }

        public Task GetTaskByName(string taskName)
        {
            foreach (var task in tasks)
            {
                if (task.Name == taskName)
                {
                    return task;
                }
            }
            return null;
        }

        public bool DeleteIdWorkerFromTasks(int idWorker)
        {
            var tasks = GetTasksByIdWorker(idWorker);
            foreach (var task in tasks)
            {
                if (task.IdWorker == idWorker)
                {
                    task.IdWorker = null;
                    return true;
                }
            }
            return false;
        }

    }
}
