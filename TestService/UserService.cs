using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIService;

namespace TestService
{
    public class UserService : IUserService
    {
        public bool CheckLogin(string username, string password)
        {
            return true;
        }

        public bool CheckUserNameExist(string username)
        {
            return true;
        }
    }
}
