using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;
using System.Data.Entity;
using ZSZ.Common;

namespace ZSZ.Service
{
    public class AdminUserService : IAdminUserService
    {
        /// <summary>
        /// 新增管理员
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="phoneNum">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="cityId">城市id</param>
        /// <returns></returns>
        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            AdminUserEntity user = new AdminUserEntity();
            user.CityId = cityId;
            user.Name = name;
            user.PhoneNum = phoneNum;
            user.Email = email;
            string salt = CommonHelper.CreateVerifyCode(5);
            user.PasswordSalt = salt;
            string pwdHash = CommonHelper.CaclMD5(salt + password);
            user.PasswordHash = pwdHash;
            using(ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(ctx);
                bool exist = service.GetAll().Any(u => u.PhoneNum == phoneNum);
                if (exist)
                {
                    throw new ArgumentException("手机号码已存在 " + phoneNum);
                }
                ctx.AdminUsers.Add(user);
                ctx.SaveChanges();
                return user.Id;
            }
        }
        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool CheckLogin(string phoneNum, string password)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(ctx);
                var user = service.GetAll().SingleOrDefault(u => u.PhoneNum == phoneNum);
                if (user == null)
                {
                    return false;
                }
                string pwdHash = CommonHelper.CaclMD5(user.PasswordSalt + password);
                if (pwdHash == user.PasswordHash)
                    return true;
                return false;
                
            }
        }
        /// <summary>
        /// 获取某个城市所有的AdminUserDTO
        /// </summary>
        /// <param name="cityId">城市Id，如果为空则为获取总部</param>
        /// <returns></returns>
        public AdminUserDTO[] GetAll(long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(ctx);
                return service.GetAll().AsNoTracking().Where(u => u.CityId == cityId).ToList().Select(u => ToDTO(u)).ToArray();
            }
        }
        /// <summary>
        /// 获取所有的AdminUserDTO
        /// </summary>
        /// <returns></returns>
        public AdminUserDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(ctx);
                return service.GetAll().AsNoTracking().ToList().Select(u => ToDTO(u)).ToArray();
            }
        }
        /// <summary>
        /// 根据id获取AdminUserDTO
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public AdminUserDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(ctx);
                AdminUserEntity user = service.GetAll().Include(u=>u.City).AsNoTracking().SingleOrDefault(u=>u.Id == id);
                if (user == null)
                    return null;
                return ToDTO(user);
            }
        }
        /// <summary>
        /// 根据手机号获取AdminUserDTO
        /// </summary>
        /// <param name="phoneNum">手机号</param>
        /// <returns></returns>
        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(ctx);
                var users = service.GetAll().Include(u=>u.City).AsNoTracking().Where(u=>u.PhoneNum == phoneNum);
                int count = users.Count();
                if (count <=0)
                    return null;
                else if(count == 1)
                    return ToDTO(users.Single());
                else
                {
                    throw new ApplicationException("找到多个手机号为" +phoneNum+ "的管理员");
                }
            }
        }
        /// <summary>
        /// 判断用户是否有某个权限
        /// </summary>
        /// <param name="adminUserId">用户id</param>
        /// <param name="permissionName">权限名</param>
        /// <returns></returns>
        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(ctx);
                var user = service.GetAll().Include(c => c.Roles).AsNoTracking().SingleOrDefault(u => u.Id == adminUserId);
                if(user == null)
                {
                    throw new ArgumentException("找不到id=" + adminUserId + "的管理员");
                }
                return user.Roles.SelectMany(r => r.Permissions).Any(p => p.Name == permissionName);
            }
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="adminUserId">删除Id</param>
        public void MarkDeleted(long adminUserId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> service = new BaseService<AdminUserEntity>(ctx);
                service.MarkDeleted(adminUserId);
            }
        }

        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">用户名</param>
        /// <param name="phoneNum">手机号</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="cityId">城市Id，如果为null，则为总部</param>
        public void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> bs
                    = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetById(id);
                if (user == null)
                {
                    throw new ArgumentException("找不到id=" + id + "的管理员");
                }
                user.Name = name;
                user.PhoneNum = phoneNum;
                user.Email = email;
                //如果密码为空 则不更新密码
                if (!string.IsNullOrEmpty(password))
                {
                    user.PasswordHash = CommonHelper.CaclMD5(user.PasswordSalt + password);
                }
                user.CityId = cityId;
                ctx.SaveChanges();
            }
        }
        private AdminUserDTO ToDTO(AdminUserEntity user)
        {
            AdminUserDTO dto = new AdminUserDTO();
            
            dto.CityId = user.CityId;
            if (user.City != null)
            {
                dto.CityName = user.City.Name;
            }
            else
            {
                dto.CityName = "总部";
            }
            dto.CreateDateTime = user.CreateDateTime;
            dto.Email = user.Email;
            dto.Id = user.Id;
            dto.LastLoginErrorDateTime = user.LastErrorDateTime;
            dto.LoginErrorTimes = user.LoginErrorTimes;
            dto.Name = user.Name;
            dto.PhoneNum = user.PhoneNum;
            return dto;
        }
    }
}
