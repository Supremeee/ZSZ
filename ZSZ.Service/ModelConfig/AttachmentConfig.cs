using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    class AttachmentConfig:EntityTypeConfiguration<AttachmentEntity>
    {
        public AttachmentConfig()
        {
            ToTable("T_Attachments");
            HasMany(u => u.Houses).WithMany(e => e.Attachments).Map(m => m.ToTable("T_HouseAttachments")
            .MapLeftKey("AttachmentId").MapRightKey("HouseId"));
            Property(u => u.IconName).HasMaxLength(50).IsRequired();
            Property(u => u.Name).HasMaxLength(50).IsRequired();

        }
    }
}
