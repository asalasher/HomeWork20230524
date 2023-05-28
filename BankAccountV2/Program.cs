using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountV2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Mock data
            var accounts = new List<string>() {"1111", "2222", "3333"};
            var passwords = new List<string>() {"pw1", "pw2", "pw3"};
            var trasactions = new List<List<decimal>>() {
                new List<decimal>() {234.12m, -222.1m},
                new List<decimal>() {1000.12m, -567.1m},
                new List<decimal>() {5000.12m, -234.1m},
            };

            int loggingAttempts = 0;
            int maxNumberLoggingAttempts = 3;
            int? userIndex = null;

            while (userIndex != null)
            {

                Console.WriteLine("Introduce your account number");
                var inputAccount = Console.ReadLine();
                int? accountIndex = null;

                for (var i = 0; i < accounts.Count; i++)
                {
                    if (accounts[i] == inputAccount)
                    {
                        accountIndex = i;
                    }
                }

                Console.WriteLine("Introduce your password");
                var inputPassword = Console.ReadLine();
                int? passwordIndex = null;

                for (var i = 0; i < passwords.Count; i++)
                {
                    if (passwords[i] == inputPassword)
                    {
                        passwordIndex = i;
                    }
                }

                if (accountIndex == null || accountIndex != passwordIndex)
                {
                    Console.WriteLine("Account number of password incorrect");
                    Console.WriteLine("Try again");
                    loggingAttempts++;
                }
                else
                {
                    userIndex = (int)accountIndex;
                }


                if (loggingAttempts == maxNumberLoggingAttempts)
                {
                    Console.WriteLine("You reached the maximum number of attempts. Try latter on.");
                    return;
                }

            }

            bool exit = false;
            var userTransactions = trasactions[(int)userIndex];
            while (!exit)
            {
                int inputAttempts = 0;
                int maxInputAttempts = 3;

                Console.WriteLine("=====OPTIONS=====");
                Console.WriteLine("1.Money income");
                Console.WriteLine("2.Money outcome");
                Console.WriteLine("3.List all movements");
                Console.WriteLine("4.List incomes");
                Console.WriteLine("5.List outcomes");
                Console.WriteLine("6.Show current money");
                Console.WriteLine("7.Exit");

                #region input validation

                string chosenOption = null;

                while (chosenOption != null)
                {
                    Console.WriteLine("Select an option");
                    string consoleInput = Console.ReadLine();
                    var allowedInputs = new List<string>() { "1", "2", "3", "4", "5", "6", "7" };

                    if (!allowedInputs.Contains(consoleInput))
                    {
                        inputAttempts++;

                        if (inputAttempts == maxInputAttempts)
                        {
                            Console.WriteLine("You reached the maximum number of attempts. Try again latter on.");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please select a number between 1 and 7");
                        }
                    }
                    else
                    {
                        chosenOption = consoleInput;
                    }
                }
                #endregion

                #region operations

                switch (chosenOption)
                {
                    case "1":
                        Console.WriteLine("Insert the amount you would like to insert:");

                        inputAttempts = 0;
                        while (true)
                        {
                            if (decimal.TryParse(Console.ReadLine(), out decimal validatedIncome) && validatedIncome > 0)
                            {
                                userTransactions.Add(validatedIncome);
                                break;
                            }
                            else
                            {
                                inputAttempts++;

                                if (inputAttempts == maxInputAttempts)
                                {
                                    Console.WriteLine("You run out of attempst. Try latter on");
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid amount. It must be a number greater that 0");
                                }
                            }
                        }

                        break;
                    case "2":
                        Console.WriteLine("Insert the amount you would like to withdraw:");

                        inputAttempts = 0;
                        while (true)
                        {
                            if (decimal.TryParse(Console.ReadLine(), out decimal validatedOutcome) && validatedOutcome > 0)
                            {
                                userTransactions.Add(-1 * validatedOutcome);
                                break;
                            }
                            else
                            {
                                inputAttempts++;

                                if (inputAttempts == maxInputAttempts)
                                {
                                    Console.WriteLine("You run out of attempst. Try latter on");
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid amount. It must be a number greater that 0");
                                }
                            }
                        }

                        break;
                    case "3":
                        Console.WriteLine("All your transacionts are:");

                        if (userTransactions.Count == 0)
                        {
                            Console.WriteLine("You have no transactions to show");
                        }
                        else
                        {
                            foreach (var transaction in userTransactions)
                            {
                                Console.WriteLine($"{transaction}");
                            }
                        }

                        break;
                    case "4":
                        var incomes = new List<decimal>();
                        foreach (var transaction in userTransactions)
                        {
                            if (transaction > 0)
                            {
                                incomes.Add(transaction);
                            }
                        }

                        if (incomes.Count == 0)
                        {
                            Console.WriteLine("You have no incomes to show");
                        }
                        else
                        {
                            Console.WriteLine("List of chronological incomes:");
                            foreach (var income in incomes)
                            {
                                Console.WriteLine($"{income}");
                            }
                        }

                        break;
                    case "5":
                        var outcomes = new List<decimal>();
                        foreach (var transaction in userTransactions)
                        {
                            if (transaction < 0)
                            {
                                outcomes.Add(transaction);
                            }
                        }

                        if (outcomes.Count == 0)
                        {
                            Console.WriteLine("You have no outcomes to show");
                        }
                        else
                        {
                            Console.WriteLine("List of chronological outcomes:");
                            foreach (var outcome in outcomes)
                            {
                                Console.WriteLine($"{outcome}");
                            }
                        }

                        break;
                    case "6":
                        decimal balance = 0;

                        foreach (var transaction in userTransactions)
                        {
                            balance += transaction;
                        }

                        Console.WriteLine($"Your current balance is: {balance}");
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
                #endregion

            }
        }
    }
}
