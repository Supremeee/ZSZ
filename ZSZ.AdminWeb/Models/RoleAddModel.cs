using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZSZ.AdminWeb.Models
{
    public class RoleAddModel
    {
        public string Name { get; set; }
        public long[] PermissionIds { get; set; }
    }
}