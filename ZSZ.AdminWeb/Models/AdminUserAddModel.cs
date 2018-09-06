using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSZ.DTO;
using ZSZ.Service.Entities;

namespace ZSZ.AdminWeb.Models
{
    public class AdminUserAddModel
    {
        public CityDTO[] Citys { get; set; }
        public RoleDTO[] Roles { get; set; }
    }
}