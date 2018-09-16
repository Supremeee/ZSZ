using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
using ZSZ.AdminWeb.Models;
using ZSZ.CommonMVC;
using ZSZ.DTO;
using ZSZ.IService;

namespace ZSZ.AdminWeb.Controllers
{
    public class AdminUserController : Controller
    {
        public IAdminUserService AdminUserService { get; set; }
        public IRoleService RoleService { get; set; }
        public ICityService CityService { get; set; }
        // GET: AdminUser
        [CheckPermission("AdminUser.List")]
        public ActionResult List()
        {
            var models = AdminUserService.GetAll();
            return View(models);
        }
        [HttpGet]
        public ActionResult Add()
        {
            var citys = CityService.GetAll().ToList();
            citys.Insert(0, new CityDTO { Id = 0, Name = "总部" });
            var roles = RoleService.GetAll();
            AdminUserAddModel model = new AdminUserAddModel()
            {
                Citys = citys.ToArray(),
                Roles = roles
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(AdminUserAddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            long? cityId = null;
            if (model.CityId != 0)
                cityId = model.CityId;

            long userId = AdminUserService.AddAdminUser(model.Name, model.PhoneNum, model.Password, model.Email, cityId);

            RoleService.AddRoleIds(userId, model.RoleIds);
            return Json(new AjaxResult { Status = "ok" });

        }

        public ActionResult CheckPhoneNum(string phoneNum,long? userId)
        {
            bool isOk = false;
            var user = AdminUserService.GetByPhoneNum(phoneNum);
            if (userId == null)
            {
                isOk = (user == null);
            }
            else
            {
                isOk = (user==null || user.Id == userId);
            }
            return Json(new AjaxResult {Status = isOk ? "ok" : "exists"});

        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var user = AdminUserService.GetById(id);
            var roleIds = RoleService.GetByAdminUserId(user.Id);
            var allRoles = RoleService.GetAll();
            var citys = CityService.GetAll().ToList();
            citys.Add(new CityDTO { Id = 0, Name = "总部" });

            AdminUserEditModel model = new AdminUserEditModel();
            model.AdminUser = user;
            model.SelectRoleIds = roleIds.Select(u => u.Id).ToArray();
            model.AllRoles = allRoles;
            model.Citys = citys.ToArray();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(AdminUserEditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            long? cityId = null;
            if (model.CityId != 0)
                cityId = model.CityId;

            AdminUserService.UpdateAdminUser(model.Id, model.Name, model.PhoneNum, model.Password, model.Email, cityId);

            RoleService.UpdateRoleIds(model.Id, model.RoleIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        public ActionResult Delete(long id)
        {
            AdminUserService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [HttpPost]
        public ActionResult BatchDelete(long[] selectIds)
        {

            foreach (long id in selectIds)
            {
                AdminUserService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}