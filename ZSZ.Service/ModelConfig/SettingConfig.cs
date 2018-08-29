using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    class SettingConfig : EntityTypeConfiguration<SettingEntity>
    {
        public SettingConfig()
        {
            ToTable("T_Settings");
            Property(u => u.Name).HasMaxLength(1024).IsRequired();
            Property(u => u.Value).IsRequired();
        }
    }
}
