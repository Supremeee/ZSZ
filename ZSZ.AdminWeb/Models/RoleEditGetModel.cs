using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class RoleEditGetModel
    {
        public RoleDTO Role { get; set; }
        public long[] PermissionIds { get; set; }
        public PermissionDTO[] AllPermissions { get; set; }
    }
}