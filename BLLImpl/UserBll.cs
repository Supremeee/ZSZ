using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
namespace BLLImpl
{
    public class UserBll : IUserBll
    {
        public void Add(string username)
        {
            Console.WriteLine("新增用户 {0}",username);
        }

        public bool CheckLogin(string username, string pwd)
        {
            Console.WriteLine("检查登录 {0}", username);
            return true;
        }
    }
}
