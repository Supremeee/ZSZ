using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    public class CityService : ICityService
    {
        /// <summary>
        /// 新增城市
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public long AddNew(string cityName)
        {
            using(ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(ctx);
                bool exist = service.GetAll().Any(u => u.Name == cityName);
                if (exist)
                {
                    throw new ArgumentException("城市名已存在");
                }
                CityEntity city = new CityEntity();
                city.Name = cityName;
                ctx.Cities.Add(city);
                ctx.SaveChanges();
                return city.Id;
            }
        }
        private CityDTO ToDTO(CityEntity cityEntity)
        {
            CityDTO cityDTO = new CityDTO();
            cityDTO.CreateDateTime = cityEntity.CreateDateTime;
            cityDTO.Id = cityEntity.Id;
            cityDTO.Name = cityEntity.Name;
            return cityDTO;
        }
        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        public CityDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(ctx);
                return service.GetAll().AsNoTracking().ToList().Select(u => ToDTO(u)).ToArray();
            }
        }
        /// <summary>
        /// 根据Id获取城市
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CityDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(ctx);
                var city =  service.GetById(id);
                if (city == null)
                    return null;
                return ToDTO(city);
            }
        }

        
    }
}
