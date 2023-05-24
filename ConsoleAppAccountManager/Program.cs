using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAccountManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            decimal balance = 0;
            var incomes = new List<decimal>();
            List<decimal> outcomes = new List<decimal>();

            while (!exit)
            {
                Console.WriteLine("=====OPTIONS=====");
                Console.WriteLine("1.Money income");
                Console.WriteLine("2.Money outcome");
                Console.WriteLine("3.List all movements");
                Console.WriteLine("4.List incomes");
                Console.WriteLine("5.List outcomes");
                Console.WriteLine("6.Show current money");
                Console.WriteLine("7.Exit");

                #region input validation
                bool isValidInput = false;
                int validatedInput = 0;

                while (!isValidInput)
                {

                    Console.WriteLine("Select an option");
                    string userInput = Console.ReadLine();

                    if (int.TryParse(userInput, out validatedInput))
                    {
                        if (validatedInput >= 1 && validatedInput <= 7)
                        {
                            isValidInput = true;
                            validatedInput = int.Parse(userInput);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please select a number between 1 and 7");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter an integer");
                    }
                
                }
                #endregion

                #region operations
                if (validatedInput == 7)
                {
                    exit = true;
                    return; // todo -> break or return?
                }
                else if (validatedInput == 6)
                {
                    Console.WriteLine("Your current balance is: {0}", balance);
                }
                else if(validatedInput == 5)
                {
                    if (outcomes.Count == 0)
                    {
                        Console.WriteLine("You have no outcomes to show");
                    }
                    else
                    {
                        Console.WriteLine("List of chronological outcomes:");
                        foreach (decimal outcome in outcomes)
                        {
                            Console.WriteLine($"{outcome}");
                        }
                    }
                }
                else if (validatedInput == 4)
                {
                    if (incomes.Count == 0)
                    {
                        Console.WriteLine("You have no incomes to show");
                    }
                    else
                    {
                        Console.WriteLine("List of chronological incomes:");
                        foreach (decimal income in incomes)
                        {
                            Console.WriteLine($"{income}");
                        }
                    }
                }
                else if (validatedInput == 3)
                {
                    if (outcomes.Count == 0)
                    {
                        Console.WriteLine("You have no outcomes to show");
                    }
                    else
                    {
                        Console.WriteLine("List of chronological outcomes:");
                        foreach (decimal outcome in outcomes)
                        {
                            Console.WriteLine($"{outcome}");
                        }
                    }

                    if (incomes.Count == 0)
                    {
                        Console.WriteLine("You have no incomes to show");
                    }
                    else
                    {
                        Console.WriteLine("List of chronological incomes:");
                        foreach (decimal income in incomes)
                        {
                            Console.WriteLine($"{income}");
                        }
                    }
                }
                else if (validatedInput == 2)
                {
                    decimal validatedOutcome;
                    bool isValidOutcomeAmount = false;
                    while (!isValidOutcomeAmount)
                    {
                        Console.WriteLine("Insert the amount you would like to withdraw:");
                        string outcomeInput = Console.ReadLine();

                        if (decimal.TryParse(outcomeInput, out validatedOutcome) && validatedOutcome > 0) //  && outcomeInput != null
                        {
                            isValidOutcomeAmount = true;
                            balance -= validatedOutcome;
                            outcomes.Add(validatedOutcome);
                        }
                        else { Console.WriteLine("Invalid amount. It must be a number greater that 0"); }
                    }
                }
                else if (validatedInput == 1)
                {
                    decimal validatedIncome;
                    bool isValidIncomeAmount = false;
                    while (!isValidIncomeAmount)
                    {
                        Console.WriteLine("Insert the amount you would like to insert:");
                        string incomeInput = Console.ReadLine();

                        if (decimal.TryParse(incomeInput, out validatedIncome) && validatedIncome > 0)
                        {
                            isValidIncomeAmount = true;
                            balance += validatedIncome;
                            incomes.Add(validatedIncome);
                        }
                        else { Console.WriteLine("Invalid amount. It must be a number greater that 0"); }
                    }
                }
                #endregion

                #region another operation question
                Console.WriteLine("Would you like to do another operation? (press 'y' if so)");
                string input = Console.ReadLine();
                if (input != "y")
                {
                    Console.WriteLine("Your current balance is: {0}", balance);
                    Console.WriteLine("Press any letter to close the window");
                    Console.ReadLine();
                    return;
                } 
                #endregion
            }
        }
    }
}
