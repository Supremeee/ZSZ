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
            var citys = CityService.GetAll();
            var roles = RoleService.GetAll();
            AdminUserAddModel model = new AdminUserAddModel()
            {
                Citys = citys,
                Roles = roles
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(AdminUserAddPostModel model)
        {

            return Json(new AjaxResult {Status = "ok"});

        }
    }
}