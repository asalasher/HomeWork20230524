using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace POOMultiUserBank
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Mock data
            List<User> users = new List<User>()
            {
                new User("1111", "pw1"),
                new User("2222", "pw2"),
                new User("3333", "pw3"),
            };

            var accountManager = new AccountManager(users);

            // Start session
            Console.WriteLine("Wellcome to your Bank");
            var session = new Session(accountManager);
            session.AuthenticateUser();

            while (session.activeUser != null)
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
                        session.activeUser.AddTransaction(AskForUnsignedDecimal());
                        break;
                    case "2":
                        Console.WriteLine("Introduce the amount you want to withdraw");
                        session.activeUser.AddTransaction(-1 * AskForUnsignedDecimal());
                        break;
                    case "3":
                        session.activeUser.PrintTransactions("all");
                        break;
                    case "4":
                        session.activeUser.PrintTransactions("income");
                        break;
                    case "5":
                        session.activeUser.PrintTransactions("outcome");
                        break;
                    case "6":
                        var balance = session.activeUser.CalculateBalance();
                        Console.WriteLine($"Your current balance is: {balance}");
                        break;
                    case "7":
                        session.Logout();
                        break;
                    default:
                        Console.WriteLine("Option not available. Please introduce a number between 1 and 7");
                        break;
                }
            }
            Console.WriteLine("Press a key to continue");
            Console.ReadLine();
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
