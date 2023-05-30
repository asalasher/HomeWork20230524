using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOMultiUserBank
{
    public class AccountManager
    {
        private List<User> Users { get; set; }
        public AccountManager(List<User> users)
        {
            Users = users;
        }

        public User GetUserByAccountNumber(string accountNumber)
        {
            foreach (var user in Users)
            {
                if ( user.AccountNumber == accountNumber)
                {
                    return user;
                }
            }
            return null;
        }

    }
}
