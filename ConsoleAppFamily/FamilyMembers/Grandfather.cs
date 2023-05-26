using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFamily
{
    internal class Grandfather
    {
        public int age;
        protected string name;
        private int id;

        public int GrandFatherAge {
            get { return age; }
            set { age = value; }
        }

        public string GrandFatherName {
            get { return name; }
            set { name = value; }
        }

        public int GrandFatherId {
            get { return id; }
            set { id = value; }
        }

    }
}
