using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOMultiUserBank
{
    internal class Session
    {
        public User activeUser;
        private int loginAttempts;
        public AccountManager accountManager;

        public Session(AccountManager accountManager)
        {
            activeUser = null;
            loginAttempts = 0;
            this.accountManager = accountManager;
        }

        public void AuthenticateUser()
        {
            do
            {
                Console.WriteLine("Insert an account number");
                string inputAccountNumber = Console.ReadLine();

                Console.WriteLine("Insert the password");
                string inputPassword = Console.ReadLine();

                User loginUser = accountManager.GetUserByAccountNumber(inputAccountNumber);
                if (loginUser == null || !loginUser.VerifyPassword(inputPassword))
                {
                    loginAttempts++;
                    Console.WriteLine("Invalid account number or password");
                    Console.WriteLine("Try again");
                }
                else
                {
                    activeUser = loginUser;
                    return;
                }
            }
            while (loginAttempts < 3);

            Console.WriteLine("You run out of attempts. Try again later");
        }

        public void Logout()
        {
            Console.WriteLine("Login out...");
            Console.WriteLine($"Your current balance is: {activeUser.CalculateBalance()}");
            activeUser = null;
        }
    }
}
