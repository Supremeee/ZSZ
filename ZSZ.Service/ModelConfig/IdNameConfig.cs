using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    public class IdNameConfig:EntityTypeConfiguration<IdNameEntity>
    {
        public IdNameConfig()
        {
            ToTable("T_IdNames");
            Property(u => u.Name).HasMaxLength(1024).IsRequired();
            Property(u => u.TypeName).HasMaxLength(1024).IsRequired();
        }
    }
}
