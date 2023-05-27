using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
{
    internal class TaskManager
    {
        private List<Task> Tasks { get; set; }
        public TaskManager()
        {
            Tasks = new List<Task>();
        }

        public bool RegisterNewTask(Task newTask)
        {
            Tasks.Add(newTask);
            return true;
        }

        public List<Task> GetTasksByIdWorker(int? idWorker)
        {
            var tasks = new List<Task>();

            foreach (var task in Tasks)
            {
                if (task.IdWorker == idWorker)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }

        public Task GetTaskByName(string taskName)
        {
            foreach (var task in Tasks)
            {
                if (task.Name == taskName)
                {
                    return task;
                }
            }
            return null;
        }

    }
}
