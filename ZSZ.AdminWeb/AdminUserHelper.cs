using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.AdminWeb
{
    public class AdminUserHelper
    {
        public static long? GetLoginUserId(HttpContextBase ctx)
        {
            return (long?)ctx.Session["LoginUserId"];
        }
    }
}