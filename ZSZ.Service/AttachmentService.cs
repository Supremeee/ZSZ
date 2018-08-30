using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;
using System.Data.Entity;

namespace ZSZ.Service
{
    public class AttachmentService : IAttachmentService
    {
        /// <summary>
        /// 获取所有的设施
        /// </summary>
        /// <returns></returns>
        public AttachmentDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AttachmentEntity> bs = new BaseService<AttachmentEntity>(ctx);
                var items = bs.GetAll().AsNoTracking();
                return items.ToList().Select(a => ToDTO(a)).ToArray();
            }
        }
        /// <summary>
        /// 获取某个房子的设施
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public AttachmentDTO[] GetAttachments(long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> bs = new BaseService<HouseEntity>(ctx);
                var house = bs.GetAll().Include(u=>u.Attachments).AsNoTracking().SingleOrDefault(u=>u.Id == houseId);
                if (house == null)
                    throw new ArgumentException("未能找到id="+houseId+"的房子");

                return house.Attachments.ToList().Select(u=>ToDTO(u)).ToArray();
            }
        }
        private AttachmentDTO ToDTO(AttachmentEntity att)
        {
            AttachmentDTO dto = new AttachmentDTO();
            dto.CreateDateTime = att.CreateDateTime;
            dto.IconName = att.IconName;
            dto.Id = att.Id;
            dto.Name = att.Name;
            return dto;
        }
    }
}
