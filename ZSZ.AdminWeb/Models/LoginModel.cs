using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ZSZ.AdminWeb.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(11,MinimumLength = 6)]
        public string PhoneNum { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(4,MinimumLength = 4)]
        public string VarifyCode { get; set; }
    }
}