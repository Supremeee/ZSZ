using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            
            long? cityId = null;
            if (model.City != 0)
                cityId = model.City;

            long userId = AdminUserService.AddAdminUser(model.Name, model.PhoneNum, model.Password, model.Email, cityId);
            RoleService.AddRoleIds(userId, model.RoleIds);
            return Json(new AjaxResult { Status = "ok" });

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
            long? cityId = null;
            if (model.City != 0)
                cityId = model.City;

            AdminUserService.UpdateAdminUser(model.Id, model.Name, model.PhoneNum, model.Password, model.Email, cityId);
            RoleService.UpdateRoleIds(model.Id, model.RoleIds);
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}