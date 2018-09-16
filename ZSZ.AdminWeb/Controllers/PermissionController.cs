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
    public class PermissionController : Controller
    {
        public IPermissionService PerSer { get; set; }
        [CheckPermission("Permission.List")]
        // GET: Permission
        public ActionResult List()
        {
            var pers = PerSer.GetAll();
            return View(pers);
        }
        [CheckPermission("Permission.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [CheckPermission("Permission.Add")]
        [HttpPost]
        public ActionResult Add(PermissionAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            PerSer.AddPermission(model.Name, model.Description);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Permission.Edit")]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var res = PerSer.GetById(id);
            PermissionEditModel model = new PermissionEditModel()
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description
            };
            return View(model);
        }
        [CheckPermission("Permission.Edit")]
        [HttpPost]
        public ActionResult Edit(PermissionEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState) });
            }
            PerSer.UpdatePermission(model.Id, model.Name, model.Description);
            return Json(new AjaxResult { Status = "ok" });
        }
        [CheckPermission("Permission.Delete")]
        [HttpPost]
        public ActionResult Delete(long id)
        {
            PerSer.MarkDeleted(id);
            return Json( new AjaxResult { Status="ok"});
        }
        [CheckPermission("Permission.BatchDelete")]
        [HttpPost]
        public ActionResult BatchDelete(long[] selectIds)
        {
            foreach (var id in selectIds)
            {
                PerSer.MarkDeleted(id);
            }
            return Json(new AjaxResult { Status = "ok" });
        }
    }
}