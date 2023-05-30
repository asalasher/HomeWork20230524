using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
{
    public class Worker
    {

        public static int TotalCount { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LeaveDate { get; set; }

        public Worker(string name, string surname, DateTime birthDate)
        {
            TotalCount++;
            Id = TotalCount;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }

    }
}
