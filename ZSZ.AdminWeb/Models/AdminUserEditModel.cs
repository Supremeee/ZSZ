using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Models
{
    public class AdminUserEditModel
    {
        public AdminUserDTO AdminUser { get; set; }
        public long[] SelectRoleIds { get; set; }
        public RoleDTO[] AllRoles { get; set; }
        public CityDTO[] Citys { get; set; }
    }
}