using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    public class RoleConfig: EntityTypeConfiguration<RoleEntity>
    {
        public RoleConfig()
        {
            ToTable("T_Roles");
            HasMany(u => u.Permissions).WithMany(p => p.Roles).Map(m => m.ToTable("T_RolePermissions").MapLeftKey("RoleId").MapRightKey("PermissionId"));
            Property(u => u.Name).HasMaxLength(60).IsRequired();
        }
    }
}
