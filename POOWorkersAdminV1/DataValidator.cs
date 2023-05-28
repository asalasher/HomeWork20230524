using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOWorkersAdminV1
{
    internal class DataValidator
    {

        public static int? AskForUnsignedInteger()
        {
            var numberOfAttempts = 0;
            var maxNumberOfAttempts = 3;
            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (int.TryParse(Console.ReadLine(), out int validatedInput) && validatedInput > 0)
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a positive integer value");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }
            Console.WriteLine("Too many attempts, try again later");
            return null;
        }

        public static decimal? AskForUnsignedDecimal()
        {
            var numberOfAttempts = 0;
            var maxNumberOfAttempts = 3;
            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal validatedInput) && validatedInput > 0)
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a positive decimal value");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }

            Console.WriteLine("Too many attempts, try again later");
            return null;
        }

        public static DateTime? AskForDate()
        {
            var numberOfAttempts = 0;
            var maxNumberOfAttempts = 3;
            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input follows the correct date format");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }

            Console.WriteLine("Too many attempts, try again later");
            return null;
        }

        public static WorkerLevel? AskForWorkerLevel()
        {
            var numberOfAttempts = 0;
            var maxNumberOfAttempts = 3;
            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (WorkerLevel.TryParse(Console.ReadLine(), out WorkerLevel validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a positive decimal value");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }

            Console.WriteLine("Too many attempts, try again later");
            return null;
        }

        public static TaskStatus? AskForTaskStatus()
        {
            Console.WriteLine("Introduce the task's status (ToDo, Doing, Done)");
            var numberOfAttempts = 0;
            var maxNumberOfAttempts = 3;
            while (numberOfAttempts < maxNumberOfAttempts)
            {
                if (WorkerLevel.TryParse(Console.ReadLine(), out TaskStatus validatedInput))
                {
                    return validatedInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please make sure your input is a positive decimal value");
                    Console.WriteLine($"{maxNumberOfAttempts - numberOfAttempts}attempts left");
                }
            }

            Console.WriteLine("Too many attempts, try again later");
            return null;
        }

    }
}
