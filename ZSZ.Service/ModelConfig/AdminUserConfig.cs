using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    class AdminUserConfig:EntityTypeConfiguration<AdminUserEntity>
    {
        public AdminUserConfig()
        {
            ToTable("T_AdminUsers");
            HasOptional(u => u.City).WithMany().HasForeignKey(e => e.CityId).WillCascadeOnDelete(false);
            HasMany(u => u.Roles).WithMany(r => r.AdminUsers).Map(m => m.ToTable("T_AdminUserRoles").MapLeftKey("AdminUserId").MapRightKey("RoleId"));
            Property(u => u.Name).HasMaxLength(50).IsRequired();
            Property(u => u.Email).HasMaxLength(30).IsRequired().IsUnicode(false);
            Property(u => u.PhoneNum).HasMaxLength(20).IsRequired().IsUnicode(false);//IsUnicode(false) 设置为varchar类型
            Property(u => u.PasswordSalt).HasMaxLength(20).IsRequired().IsUnicode(false);
            Property(u => u.PasswordHash).HasMaxLength(100).IsRequired().IsUnicode(false);
        }
    }
}
