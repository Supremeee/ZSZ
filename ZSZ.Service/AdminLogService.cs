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
    public class AdminLogService : IAdminLogService
    {
        public long AddNew(long adminUserId, string message)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                
                AdminLogEntity adminLog = new AdminLogEntity();
                adminLog.AdminUserId = adminUserId;
                adminLog.Message = message;
                ctx.AdminLogs.Add(adminLog);
                ctx.SaveChanges();
                return adminLog.Id;
            }
            
        }
        private AdminLogDTO ToDTO(AdminLogEntity log)
        {
            AdminLogDTO dto = new AdminLogDTO();
            dto.AdminUserId = log.AdminUserId;
            dto.AdminUserName = log.AdminUser.Name;
            dto.AdminUserPhoneNum = log.AdminUser.PhoneNum;
            dto.CreateDateTime = log.CreateDateTime;
            dto.Id = log.Id;
            dto.Message = log.Message;
            return dto;
        }
        public AdminLogDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminLogEntity> service = new BaseService<AdminLogEntity>(ctx);
                var log = service.GetById(id);
                if (log == null)
                    return null;
                return ToDTO(log);
            }
        }
    }
}
