using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.App_Start;
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
        [CheckPermission("Role.List")]
        public ActionResult List()
        {
            var roles = roleService.GetAll();
            return View(roles);
        }
        [CheckPermission("Role.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            var pers = perService.GetAll();
            return View(pers);
        }
        [CheckPermission("Role.Add")]
        [HttpPost]
        public ActionResult Add(RoleAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            long id = roleService.AddNew(model.Name);
            perService.AddPermIds(id, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Role.Edit")]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var role = roleService.GetById(id);
            var permissions = perService.GetAll();
            var hasPermissionIds = perService.GetByRoleId(role.Id).Select(u=>u.Id).ToArray();
            RoleEditGetModel model = new RoleEditGetModel
            {
                Role=role,
                AllPermissions = permissions,
                PermissionIds = hasPermissionIds
            };
            return View(model);
        }
        [CheckPermission("Role.Edit")]
        [HttpPost]
        public ActionResult Edit(RoleEditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            roleService.Update(model.Id, model.Name);
            perService.UpdatePermIds(model.Id, model.PermissionIds);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Role.Delete")]
        [HttpPost]
        public ActionResult Delete(long id)
        {
            roleService.MarkDeleted(id);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Role.BatchDelete")]
        [HttpPost]
        public ActionResult BatchDelete(long[] selectIds)
        {
            foreach (var id in selectIds)
            {
                roleService.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}