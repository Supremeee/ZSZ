using System;
using System.Collections.Generic;
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

        public CityDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public CityDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        
    }
}
