using System;
using System.Collections.Generic;

namespace POOWorkersAdminV1
{
    public class WorkerManager
    {
        private List<ItWorker> Workers { get; set; }
        public WorkerManager(List<ItWorker> workers)
        {
            Workers = workers;
        }

        public ItWorker GetWorkerById(int id)
        {
            foreach (var worker in Workers)
            {
                if (worker.Id == id)
                {
                    return worker;
                }
            }
            return null;
        }

        public bool RegisterNewWorker(ItWorker worker)
        {

            if ((DateTime.Today.Year - worker.BirthDate.Year) <= 18
                && DateTime.Today.DayOfYear <= worker.BirthDate.DayOfYear)
            {
                Console.WriteLine("Worker too your to be an It worker");
                return false;
            }

            Workers.Add(worker);
            return true;
        }

        public bool UnregisterWorkerById(int idWorker)
        {

            foreach (var worker in Workers)
            {
                if (worker.Id == idWorker)
                {
                    // TODO - check this or .RemoveAt()
                    Workers.Remove(worker);
                    return true;
                }
            }
            return false;
        }

    }
}
