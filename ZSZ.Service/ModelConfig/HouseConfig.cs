using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;
namespace ZSZ.Service.ModelConfig
{
    public class HouseConfig : EntityTypeConfiguration<HouseEntity>
    {
        public HouseConfig()
        {
            ToTable("T_Houses");
            HasRequired(u => u.Community).WithMany().HasForeignKey(e => e.CommunityId).WillCascadeOnDelete(false);
            HasRequired(u => u.RoomType).WithMany().HasForeignKey(e => e.RoomTypeId).WillCascadeOnDelete(false);
            HasRequired(u => u.Status).WithMany().HasForeignKey(e => e.StatusId).WillCascadeOnDelete(false);
            HasRequired(u => u.DecorateStatus).WithMany().HasForeignKey(e => e.DecorateStatusId).WillCascadeOnDelete(false);
            HasRequired(u => u.Type).WithMany().HasForeignKey(u => u.TypeId).WillCascadeOnDelete(false);
            Property(u => u.Address).HasMaxLength(128).IsRequired();
            Property(u => u.Description).IsOptional();
            Property(u => u.Direction).HasMaxLength(20).IsRequired();
            Property(u => u.OwnerName).HasMaxLength(20).IsRequired();
            Property(u => u.OwnerPhoneNum).HasMaxLength(20).IsRequired().IsUnicode(false);
        }
    }
}
