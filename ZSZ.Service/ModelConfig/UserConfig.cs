using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    public class UserConfig : EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            ToTable("T_Users");
            HasRequired(u => u.City).WithMany().HasForeignKey(u => u.CityId).WillCascadeOnDelete(false);
            Property(u => u.PasswordHash).HasMaxLength(100).IsRequired();
            Property(u => u.PasswordSalt).HasMaxLength(20).IsRequired();
            Property(u => u.PhoneNum).HasMaxLength(20).IsRequired().IsUnicode(false);
        }
    }
}
