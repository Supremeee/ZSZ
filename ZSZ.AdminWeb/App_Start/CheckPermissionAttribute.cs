using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace ZSZ.AdminWeb.App_Start
{
    /// <summary>
    ///这个属性应用到方法上，一个方法上还可以应用多个
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = true)]
    public class CheckPermissionAttribute:Attribute
    {
        public string Permission { get; set; }

        public CheckPermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}