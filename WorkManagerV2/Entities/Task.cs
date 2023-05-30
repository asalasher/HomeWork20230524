using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
{

    public class Task
    {
        private string _technology;

        public static int TotalCount;
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public string Description { get; set; }
        public string Technology
        {
            get
            {
                return _technology;
            }

            set
            {
                _technology = value.ToLower();
            }
        }
        public int? IdWorker { get; set; }

        public Task(string name, string description, string technology)
        {
            TotalCount++;
            Id = TotalCount;
            Name = name;
            Status = TaskStatus.ToDo;
            Description = description;
            Technology = technology;
            IdWorker = null;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Description: {Description} | Status: {Status} | IdWorker: {IdWorker} |";
        }
    }
}
