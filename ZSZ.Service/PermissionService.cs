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
    public class PermissionService : IPermissionService
    {
        public void AddPermIds(long roleId, long[] permIds)
        {
            using(ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> roleBS = new BaseService<RoleEntity>(ctx);
                var role = roleBS.GetById(roleId);
                if (role == null)
                    throw new ArgumentException("roleId不存在 " + roleId);
                if (permIds != null)
                {
                    BaseService<PermissionEntity> permissionBS = new BaseService<PermissionEntity>(ctx);
                    var pers = permissionBS.GetAll().Where(u => permIds.Contains(u.Id)).ToArray();
                    foreach (var per in pers)
                    {
                        role.Permissions.Add(per);
                    }
                }
                
                ctx.SaveChanges();
            }
        }

        public long AddPermission(string permName, string description)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> permissionBS = new BaseService<PermissionEntity>(ctx);
                var exist = permissionBS.GetAll().Any(u => u.Name == permName);
                if (exist)
                    throw new ArgumentException("权限项已经存在");
                PermissionEntity entity = new PermissionEntity();
                entity.Name = permName;
                entity.Description = description;
                ctx.Permissions.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }
        }

        public PermissionDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> permissionBS = new BaseService<PermissionEntity>(ctx);
                return permissionBS.GetAll().ToList().Select(u => ToDTO(u)).ToArray();
            }
        }

        public PermissionDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> permissionBS = new BaseService<PermissionEntity>(ctx);
                var per = permissionBS.GetById(id);
                return per == null ? null : ToDTO(per);
            }
        }

        public PermissionDTO GetByName(string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> permissionBS = new BaseService<PermissionEntity>(ctx);
                var per = permissionBS.GetAll().SingleOrDefault(u => u.Name == name);
                return per == null ? null : ToDTO(per);
            }
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> roleBS = new BaseService<RoleEntity>(ctx);
                return roleBS.GetById(roleId).Permissions.ToList().Select(u=>ToDTO(u)).ToArray();
            }
        }

        public void MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> service = new BaseService<PermissionEntity>(ctx);
                service.MarkDeleted(id);
            }
        }

        public void UpdatePermIds(long roleId, long[] permIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> roleBS = new BaseService<RoleEntity>(ctx);
                var role = roleBS.GetById(roleId);
                if (role == null)
                    throw new ArgumentException("roleId不存在 " + roleId);
                role.Permissions.Clear();
                if (permIds != null)
                {
                    BaseService<PermissionEntity> permissionBS = new BaseService<PermissionEntity>(ctx);
                    var pers = permissionBS.GetAll().Where(u => permIds.Contains(u.Id)).ToArray();
                    foreach (var per in pers)
                    {
                        role.Permissions.Add(per);
                    }
                }
                
                ctx.SaveChanges();
            }
        }

        public void UpdatePermission(long id, string perName, string description)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> service = new BaseService<PermissionEntity>(ctx);
                var per = service.GetById(id);
                if (per == null)
                    throw new ArgumentException("权限项不存在id=" + id);
                per.Name = perName;
                per.Description = description;
                ctx.SaveChanges();
            }
        }

        private PermissionDTO ToDTO(PermissionEntity p)
        {
            PermissionDTO dto = new PermissionDTO();
            dto.CreateDateTime = p.CreateDateTime;
            dto.Description = p.Description;
            dto.Id = p.Id;
            dto.Name = p.Name;
            return dto;
        }
    }
}
