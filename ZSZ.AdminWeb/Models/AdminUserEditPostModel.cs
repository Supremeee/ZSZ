using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ZSZ.AdminWeb.Models
{
    public class AdminUserEditPostModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [RegularExpression("^(((13[0-9]{1})|(15[0-35-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\\d{8})$")]
        public string PhoneNum { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        public long CityId { get; set; }
        public long[] RoleIds { get; set; }
    }
}