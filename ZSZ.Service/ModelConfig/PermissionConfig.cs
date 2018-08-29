using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    public class PermissionConfig : EntityTypeConfiguration<PermissionEntity>
    {
        public PermissionConfig()
        {
            ToTable("T_Permissions");
            Property(u => u.Description).HasMaxLength(1024).IsOptional();
            Property(u => u.Name).HasMaxLength(50).IsRequired();

        }
    }
}
