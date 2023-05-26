using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFamily.FamilyMembers
{
    internal class Son: Father
    {
        public Son()
        {
            SonSchoolGrade = 2;
            SonNumberOfTeeth = 15;
            SonNumberOfFingers = 20;
            FatherHeight = 180;
            FatherWeight = 88.2m;
            FatherHairColor = "brown";
            GrandFatherAge = 80;
            GrandFatherName = "Pablo";
            GrandFatherId = 1;
        }

        public int schoolGrade;
        private int numOfTeeth;
        protected int numOfFingers;

        public int SonSchoolGrade {
            get { return schoolGrade; }
            set { schoolGrade = value; }
        }

        public int SonNumberOfTeeth {
            get { return numOfTeeth; }
            set { numOfTeeth = value; }
        }

        public int SonNumberOfFingers {
            get { return numOfFingers; }
            set { numOfFingers = value; }
        }

        public void printAllValues()
        {
            Console.WriteLine($"The son's school grade is: {SonSchoolGrade}");
            Console.WriteLine($"The son's number of teeth is: {SonNumberOfTeeth}");
            Console.WriteLine($"The son's number of fingers is: {SonNumberOfFingers}");
            Console.WriteLine($"The father's height is: {FatherHeight}");
            Console.WriteLine($"The father's hair color is: {FatherHairColor}");
            Console.WriteLine($"The father's weight is: {FatherWeight}");
            Console.WriteLine($"The grandfather's age is {GrandFatherAge}");
            Console.WriteLine($"The grandfather's name is {GrandFatherName}");
            Console.WriteLine($"The grandfather's id is {GrandFatherId}");
        }

        public void ModifyAField(string option)
        {
            switch (option)
            {
                case "1":
                    Console.WriteLine("Introduce a new school grade for the son. It MUST be an integer");
                    SonSchoolGrade = AskForInteger();
                    break;
                case "2":
                    Console.WriteLine("Introduce a new number of teeth for the son. It MUST be an integer");
                    SonNumberOfTeeth = AskForInteger();
                    break;
                case "3":
                    Console.WriteLine("Introduce a new number of fingers for the son. It MUST be an integer");
                    SonNumberOfFingers = AskForInteger();
                    break;
                case "4":
                    Console.WriteLine("Introduce a new height for the father in cms. It MUST be an integer");
                    FatherHeight = AskForInteger();
                    break;
                case "5":
                    Console.WriteLine("Introduce a new hair color for the father");
                    FatherHairColor = Console.ReadLine();
                    break;
                case "6":
                    Console.WriteLine("Introduce a new weight for the father in kgs. It MUST be a decimal");
                    FatherWeight = AskForDecimal();
                    break;
                case "7":
                    Console.WriteLine("Introduce a new age for the grandfather. It MUST be an integer");
                    GrandFatherAge = AskForInteger();
                    break;
                case "8":
                    Console.WriteLine("Introduce a new name for the grandfather");
                    GrandFatherName = Console.ReadLine();
                    break;
                case "9":
                    Console.WriteLine("Introduce a new id for the grandfather. It MUST be an integer");
                    GrandFatherAge = AskForInteger();
                    break;
                default:
                    Console.WriteLine("Number not available");
                    break;
            }
        }

        protected int AskForInteger()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is an integer number");
                }
            }
        }

        protected decimal AskForDecimal()
        {
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a decimal number");
                }
            }
        }
    }
}
