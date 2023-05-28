using System;
using System.Collections.Generic;

namespace POOWorkersAdminV1
{
    public class ItWorker : Worker
    {
        private WorkerLevel _level;

        public int YearsOfExperience { get; set; }
        public List<string> TechKnowleges { get; set; }
        public WorkerLevel Level
        {
            get
            {
                return _level;
            }
            set
            {
                if (YearsOfExperience < 5)
                {
                    Console.WriteLine("Worker has not enough experience to be Senior");
                }
                else
                {
                    _level = value;
                }
            }
        }

        public ItWorker(string name, string surname, DateTime birthDate, int yearsOfExperience, List<string> techKnowleges, WorkerLevel level) : base(name, surname, birthDate)
        {
            YearsOfExperience = yearsOfExperience;
            TechKnowleges = techKnowleges;
            Level = level;
        }
    }
}
