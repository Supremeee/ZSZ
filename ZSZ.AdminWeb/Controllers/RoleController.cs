using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.Models;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        public IRoleService roleService { get; set; }
        public IPermissionService perService { get; set; }
        // GET: Role
        public ActionResult List()
        {
            var roles = roleService.GetAll();
            return View(roles);
        }
        [HttpGet]
        public ActionResult Add()
        {
            var pers = perService.GetAll();
            return View(pers);
        }
        [HttpPost]
        public ActionResult Add(RoleAddModel model)
        {
            long id = roleService.AddNew(model.Name);
            perService.AddPermIds(id, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }

        
    }
}