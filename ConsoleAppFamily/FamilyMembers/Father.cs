using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFamily.FamilyMembers
{
    internal class Father: Grandfather
    {
        public int height;
        private decimal weight;
        protected string hairColor;

        public int FatherHeight {
            get { return height; }
            set
            {
                if (value >= 200)
                {
                    height = 200;
                }
                else
                {
                    height = value;
                }
            }
        }

        public decimal FatherWeight {
            get { return weight; }
            set { weight = value; }
        }

        public string FatherHairColor {
            get { return hairColor; }
            set { hairColor = value; }
        }

    }
}
