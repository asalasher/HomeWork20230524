using ConsoleAppFamily.FamilyMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFamily
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Son son = new Son();

            Console.WriteLine("Welcome");
            Console.WriteLine("An instance of the class 'Son' has been created with some default values");

            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Show All The Values");
                Console.WriteLine("2. Modify The Values");
                Console.WriteLine("3. Exit");

                switch (AskForInteger(1,3))
                {
                    case 1:
                        son.printAllValues();
                        break;
                    case 2:
                        Console.WriteLine("Select which value you would like to modify:");

                        Console.WriteLine("1. Modify the son's school grade");
                        Console.WriteLine("2. Modify the son's number of teeth");
                        Console.WriteLine("3. Modify the son's number of fingers");

                        Console.WriteLine("4. Modify the father's height");
                        Console.WriteLine("5. Modify the father's hair color");
                        Console.WriteLine("6. Modify the father's weight");

                        Console.WriteLine("7. Modify the grandfather's age");
                        Console.WriteLine("8. Modify the grandfather's name");
                        Console.WriteLine("9. Modify the grandfather's id");

                        son.ModifyAField(Console.ReadLine());
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Option not valid");
                        break;
                }
            }
        }

        public static int AskForInteger(int minValue, int maxValue)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int validatedInput) && validatedInput <= maxValue && validatedInput >= minValue)
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine($"Invalid input. Please make sure your input is an integer between {minValue} and {maxValue}");
                }
            }
        }

    }
}
