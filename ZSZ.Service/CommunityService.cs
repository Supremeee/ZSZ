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
    public class CommunityService : ICommunityService
    {
        /// <summary>
        /// 获取某个区域的所有小区
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public CommunityDTO[] GetByRegionId(long regionId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CommunityEntity> service = new BaseService<CommunityEntity>(ctx);
                var cites = service.GetAll().AsNoTracking().Where(u => u.RegionId == regionId);
                return cites.Select(c => new CommunityDTO
                {
                    BuiltYear = c.BuiltYear,
                    CreateDateTime = c.CreateDateTime,
                    Id = c.Id,
                    Location = c.Location,
                    Name = c.Name,
                    RegionId = c.RegionId,
                    Traffic = c.Traffic
                }).ToArray();
            }
        }
    }
}
