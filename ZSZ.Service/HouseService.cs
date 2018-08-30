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
    public class HouseService : IHouseService
    {
        public long AddNew(HouseAddNewDTO house)
        {
            HouseEntity houseEntity = new HouseEntity();
            houseEntity.Address = house.Address;
            houseEntity.Area = house.Area;
            using(ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AttachmentEntity> attBS = new BaseService<AttachmentEntity>(ctx);
                var atts = attBS.GetAll().Where(u => house.AttachmentIds.Contains(u.Id));
                foreach (var att in atts)
                {
                    houseEntity.Attachments.Add(att);
                }
                houseEntity.CheckInDateTime = house.CheckInDateTime;
                houseEntity.CommunityId = house.CommunityId;
                houseEntity.DecorateStatusId = house.DecorateStatusId;
                houseEntity.Description = house.Description;
                houseEntity.Direction = house.Direction;
                houseEntity.FloorIndex = house.FloorIndex;
                //houseEntity.HousePics 新增后再单独添加
                houseEntity.LookableDateTime = house.LookableDateTime;
                houseEntity.MonthRent = house.MonthRent;
                houseEntity.OwnerName = house.OwnerName;
                houseEntity.OwnerPhoneNum = house.OwnerPhoneNum;
                houseEntity.RoomTypeId = house.RoomTypeId;
                houseEntity.StatusId = house.StatusId;
                houseEntity.TotalFloorCount = house.TotalFloorCount;
                houseEntity.TypeId = house.TypeId;
                ctx.Houses.Add(houseEntity);
                ctx.SaveChanges();
                return houseEntity.Id;
            }

        }

        public long AddNewHousePic(HousePicDTO housePic)
        {
            HousePicEntity entity = new HousePicEntity();
            entity.HouseId = housePic.Id;
            entity.ThumbUrl = housePic.ThumbUrl;
            entity.Url = housePic.Url;
            using(ZSZDbContext ctx = new ZSZDbContext())
            {
                
                ctx.HousePics.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }
        }

        public void DeleteHousePic(long housePicId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                HousePicEntity entity = new HousePicEntity();
                entity.Id = housePicId;
                ctx.Entry(entity).State = EntityState.Deleted;
                ctx.SaveChanges();

                
            }
        }

        public HouseDTO GetById(long id)
        {
            using(ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(ctx);
                var house = service.GetAll().Include(u=>u.Attachments).Include(h => h.Community).Include(nameof(HouseEntity.Community) + "" + nameof(CommunityEntity.Region) + "." + nameof(RegionEntity.City)).Include(nameof(HouseEntity.Community) + "" + nameof(CommunityEntity.Region)).Include(h => h.DecorateStatus).Include(h => h.RoomType).Include(h => h.Status).Include(h => h.Type).Include(h => h.HousePics).SingleOrDefault(u => u.Id == id);
                if (house == null)
                    return null;
                return ToDTO(house);
            }
        }

        public long GetCount(long cityId, DateTime startDateTime, DateTime endDateTime)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(ctx);
                var count = service.GetAll().LongCount(u => u.Community.Region.CityId == cityId && u.CreateDateTime >= startDateTime && u.CreateDateTime <= endDateTime);
                return count;
            }
        }

        public HouseDTO[] GetPagedData(long cityId, long typeId, int pageSize, int currentIndex)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(ctx);
                var houses = service.GetAll().Include(u => u.Attachments).Include(u => u.Community).Include(nameof(HouseEntity.Community) + "." + nameof(CommunityEntity.Region) + "." + nameof(RegionEntity.City)).Include(nameof(HouseEntity.Community) + "." + nameof(CommunityEntity.Region)).Include(u => u.DecorateStatus).Include(u => u.HousePics).Include(u => u.RoomType).Include(u => u.Status).Include(u => u.Type).Where(u => u.Community.Region.CityId == cityId && u.TypeId == typeId).OrderByDescending(u => u.CreateDateTime).Skip(currentIndex).Take(pageSize);
                return houses.ToList().Select(u => ToDTO(u)).ToArray();
            }
        }

        public HousePicDTO[] GetPics(long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                /*
               BaseService<HousePicEntity> bs = new BaseService<HousePicEntity>(ctx);
               return bs.GetAll().AsNoTracking().Where(p => p.HouseId == houseId)
                   .Select(p => new HousePicDTO
                   {
                       CreateDateTime = p.CreateDateTime,
                       HouseId = p.HouseId,
                       Id = p.Id,
                       ThumbUrl = p.ThumbUrl,
                       Url = p.Url
                   })
                   .ToArray();*/
                BaseService<HouseEntity> bs = new BaseService<HouseEntity>(ctx);
                return bs.GetById(houseId).HousePics.Select(p => new HousePicDTO
                {
                    CreateDateTime = p.CreateDateTime,
                    HouseId = p.HouseId,
                    Id = p.Id,
                    ThumbUrl = p.ThumbUrl,
                    Url = p.Url
                }).ToArray();
            }
        }

        public long GetTotalCount(long cityId, long typeId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(ctx);
                var count = service.GetAll().LongCount(u => u.Community.Region.CityId == cityId && u.TypeId == typeId);
                return count;
            }
        }

        public void MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(ctx);
                service.MarkDeleted(id);
            }
        }

        public HouseSearchResult Search(HouseSearchOptions options)
        {
            throw new NotImplementedException();
        }

        public void Update(HouseDTO house)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> service = new BaseService<HouseEntity>(ctx);
                HouseEntity entity = service.GetById(house.Id);
                entity.Area = house.Area;
                entity.Attachments.Clear();
                var atts = ctx.Attachments.Where(u => u.IsDeleted == false && house.AttachmentIds.Contains(u.Id));
                foreach (var att in atts)
                {
                    entity.Attachments.Add(att);
                }
                entity.CheckInDateTime = house.CheckInDateTime;
                entity.CommunityId = house.CommunityId;
                entity.DecorateStatusId = house.DecorateStatusId;
                entity.Description = house.Description;
                entity.Direction = house.Direction;
                entity.FloorIndex = house.FloorIndex;
                entity.LookableDateTime = house.LookableDateTime;
                entity.MonthRent = house.MonthRent;
                entity.OwnerName = house.OwnerName;
                entity.OwnerPhoneNum = house.OwnerPhoneNum;
                entity.RoomTypeId = house.RoomTypeId;
                entity.StatusId = house.StatusId;
                entity.TotalFloorCount = house.TotalFloorCount;
                entity.TypeId = house.TypeId;
                ctx.SaveChanges();
            }
        }
        private HouseDTO ToDTO(HouseEntity entity)
        {
            HouseDTO dto = new HouseDTO();
            dto.Address = entity.Address;
            dto.Area = entity.Area;
            dto.AttachmentIds = entity.Attachments.Select(a => a.Id).ToArray();
            dto.CheckInDateTime = entity.CheckInDateTime;
            dto.CityId = entity.Community.Region.CityId;
            dto.CityName = entity.Community.Region.City.Name;
            dto.CommunityBuiltYear = entity.Community.BuiltYear;
            dto.CommunityId = entity.CommunityId;
            dto.CommunityLocation = entity.Community.Location;
            dto.CommunityName = entity.Community.Name;
            dto.CommunityTraffic = entity.Community.Traffic;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.DecorateStatusId = entity.DecorateStatusId;
            dto.DecorateStatusName = entity.DecorateStatus.Name;
            dto.Description = entity.Description;
            dto.Direction = entity.Direction;
            var firstPic = entity.HousePics.FirstOrDefault();
            if (firstPic != null)
            {
                dto.FirstThumbUrl = firstPic.ThumbUrl;
            }
            dto.FloorIndex = entity.FloorIndex;
            dto.Id = entity.Id;
            dto.LookableDateTime = entity.LookableDateTime;
            dto.MonthRent = entity.MonthRent;
            dto.OwnerName = entity.OwnerName;
            dto.OwnerPhoneNum = entity.OwnerPhoneNum;
            dto.RegionId = entity.Community.RegionId;
            dto.RegionName = entity.Community.Region.Name;
            dto.RoomTypeId = entity.RoomTypeId;
            dto.RoomTypeName = entity.RoomType.Name;
            dto.StatusId = entity.StatusId;
            dto.StatusName = entity.Status.Name;
            dto.TotalFloorCount = entity.TotalFloorCount;
            dto.TypeId = entity.TypeId;
            dto.TypeName = entity.Type.Name;
            return dto;
        }
    }
}
