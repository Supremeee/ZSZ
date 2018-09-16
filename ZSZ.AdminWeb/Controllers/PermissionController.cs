﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.AdminWeb.Models;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.AdminWeb.Controllers
{
    public class PermissionController : Controller
    {
        public IPermissionService PerSer { get; set; }
        // GET: Permission
        public ActionResult List()
        {
            var pers = PerSer.GetAll();
            return View(pers);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

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
        [HttpPost]
        public ActionResult Delete(long id)
        {
            PerSer.MarkDeleted(id);
            return Json( new AjaxResult { Status="ok"});
        }
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