using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
{
    internal class Task
    {
        public static int TotalCount;
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public string Description { get; set; }
        public int? IdWorker { get; set; }

        public Task(string name, string description)
        {
            TotalCount++;
            Id = TotalCount;
            Name = name;
            Status = TaskStatus.ToDo;
            Description = description;
            IdWorker = null;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Name: {Name} | Description: {Description} | Status: {Status} | IdWorker: {IdWorker} |";
        }
    }
}
