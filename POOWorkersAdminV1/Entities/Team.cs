using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POOWorkersAdminV1
{

    public class Team
    {

        private ItWorker _manager;
        public static int TotalCount;
        public int Id;
        public ItWorker Manager {
            get { return _manager; }
            set
            {
                if (value.Level == WorkerLevel.Senior)
                {
                    _manager = value;
                }
                else
                {
                    Console.WriteLine("Only Senior It workers can be managers");
                }
            }
        }
        public List<ItWorker> Technicians { get; set; }
        public string Name { get; set; }

        public Team(ItWorker manager, string name)
        {
            TotalCount++;
            Id = TotalCount;
            Manager = manager;
            Technicians = new List<ItWorker>();
            Name = name;
        }

        public bool AddTechnician(ItWorker technician)
        {
            if (IsWorkerInTechnicians(technician.Id))
            {
                return false;
            }

            Technicians.Add(technician);
            return true;
        }

        public bool IsWorkerInTechnicians(int idWorker)
        {
            foreach (var worker in Technicians)
            {
                if (worker.Id == idWorker) {  return true; }
            }

            return false;
        }   

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Manager id: {Manager.Id} | Manger name: {Manager.Name}";
        }

    }
}
