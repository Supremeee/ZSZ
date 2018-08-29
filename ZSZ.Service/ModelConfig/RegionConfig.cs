using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    public class RegionConfig: EntityTypeConfiguration<RegionEntity>
    {
        public RegionConfig()
        {
            ToTable("T_Regions");
            HasRequired(u => u.City).WithMany().HasForeignKey(u => u.CityId).WillCascadeOnDelete(false);
            Property(u => u.Name).HasMaxLength(50).IsRequired();
        }
    }
}
