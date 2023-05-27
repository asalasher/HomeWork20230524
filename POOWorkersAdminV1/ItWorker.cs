using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
{
    internal class ItWorker : Worker
    {
        public int YearsOfExperience { get; set; }
        public List<string> TechKnowleges { get; set; }
        public WorkerLevel Level { get; set; }

        public ItWorker(string name, string surname, DateTime birthDate, int yearsOfExperience, List<string> techKnowleges, WorkerLevel level) : base(name, surname, birthDate)
        {
            YearsOfExperience = yearsOfExperience;
            TechKnowleges = techKnowleges;
            Level = level;
        }
    }
}
