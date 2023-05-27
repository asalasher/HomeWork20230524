using POOMultiUserBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace POOSingleUserBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User activeUser = new User();

            var exit = false;
            do
            {
                // Options
                Console.WriteLine("=====================");
                Console.WriteLine("Introduce an option");
                Console.WriteLine("1. Insert money");
                Console.WriteLine("2. Withdraw money");
                Console.WriteLine("3. List all movements");
                Console.WriteLine("4. List all incomes");
                Console.WriteLine("5. List all outcomes");
                Console.WriteLine("6. Show current balance");
                Console.WriteLine("7. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Introduce the amount you want to insert");
                        activeUser.AddTransaction(AskForUnsignedDecimal());
                        break;
                    case "2":
                        Console.WriteLine("Introduce the amount you want to withdraw");
                        activeUser.AddTransaction(-1 * AskForUnsignedDecimal());
                        break;
                    case "3":
                        activeUser.PrintTransactions("all");
                        break;
                    case "4":
                        activeUser.PrintTransactions("income");
                        break;
                    case "5":
                        activeUser.PrintTransactions("outcome");
                        break;
                    case "6":
                        Console.WriteLine($"Your current balance is: {activeUser.CalculateBalance()}");
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Option not available. Please introduce a number between 1 and 7");
                        break;
                }

                Console.WriteLine("Is there any other operation you would like to do ? (press 'y' if so)");
                if (Console.ReadLine() != "y")
                {
                    exit = true;
                }

            } while (!exit);
        }

        public static int AskForUnsignedInteger()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int validatedInput) && validatedInput > 0)
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a positive integer value");
                }
            }
        }

        public static decimal AskForUnsignedDecimal()
        {
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal validatedInput) && validatedInput > 0)
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a positive decimal value");
                }
            }
        }

    }
}
