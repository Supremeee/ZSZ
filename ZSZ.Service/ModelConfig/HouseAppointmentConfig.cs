using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    class HouseAppointmentConfig:EntityTypeConfiguration<HouseAppointmentEntity>
    {
        public HouseAppointmentConfig()
        {
            ToTable("T_HouseAppointments");
            HasRequired(u => u.User).WithMany().HasForeignKey(u => u.UserId).WillCascadeOnDelete(false);
            HasRequired(u => u.House).WithMany().HasForeignKey(u => u.HouseId).WillCascadeOnDelete(false);
            HasOptional(u => u.FollowAdminUser).WithMany().HasForeignKey(u => u.FollowAdminUserId).WillCascadeOnDelete(false);
            Property(u => u.Name).HasMaxLength(20).IsRequired();
            Property(u=>u.PhoneNum).HasMaxLength(20).IsRequired();
            Property(u=>u.Status).HasMaxLength(20).IsRequired();
        }
    }
}
