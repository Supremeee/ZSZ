using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    class HousePicConfig : EntityTypeConfiguration<HousePicEntity>
    {
        public HousePicConfig()
        {
            ToTable("T_HousePics");
            HasRequired(p => p.House).WithMany(p => p.HousePics).HasForeignKey(u => u.HouseId).WillCascadeOnDelete(false);
            Property(p => p.ThumbUrl).HasMaxLength(1024).IsRequired();
            Property(p => p.Url).HasMaxLength(1024).IsRequired();
        }
    }
}
