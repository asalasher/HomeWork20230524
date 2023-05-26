using POOSingleUserBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace POOMultiUserBank
{
    public class User
    {
        public string AccountNumber { get; set; }
        private List<Transaction> Transactions { get; set; }

        public User()
        {
            Transactions = new List<Transaction>();
        }

        public void AddTransaction(decimal quantity)
        {
            var transaction = new Transaction(quantity);
            Transactions.Add(transaction);
            return;
        }

        public decimal CalculateBalance()
        {
            decimal balance = 0.0m;
            foreach (Transaction transaction in Transactions)
            {
                balance += transaction.Quantity;
            }
            return balance;
        }

        //TODO - hacer un enum para outcome or income
        public void PrintTransactions(string transactionType)
        {
            if (Transactions.Count == 0)
            {
                Console.WriteLine("There are no transactions to show");
                return;
            }
            foreach (Transaction transaction in Transactions)
            {
                switch (transactionType)
                {
                    case "income":
                        if (transaction.Quantity > 0)
                        {
                            Console.WriteLine(transaction.ToString());
                        }
                        break;
                    case "outcome":
                        if (transaction.Quantity < 0)
                        {
                            Console.WriteLine(transaction.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine(transaction.ToString());
                        break;
                }
            }
            return;
        }
    }
}
