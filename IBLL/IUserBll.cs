using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IUserBll
    {
        bool CheckLogin(string username, string pwd);
        void Add(string username);
    }
}
