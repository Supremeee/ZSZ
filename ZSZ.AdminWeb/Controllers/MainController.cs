using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaGen;
using ZSZ.AdminWeb.Models;
using ZSZ.Common;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.AdminWeb.Controllers
{
    public class MainController : Controller
    {
        public IAdminUserService AdminUserService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult {Status = "error", ErrorMsg = MVCHelper.GetValidMsg(ModelState)});
            }
            if (TempData["VarifyCode"] == null || model.VarifyCode != (string)TempData["VarifyCode"])
            {
                return Json(new AjaxResult {Status = "error", ErrorMsg = "验证码错误"});
            }
            bool result = AdminUserService.CheckLogin(model.PhoneNum, model.Password);
            if (result)
            {
                Session["LoginUserId"] = AdminUserService.GetByPhoneNum(model.PhoneNum).Id;
                return Json(new AjaxResult { Status = "ok", Data = "/Main/Index" });
            }
            else
            {
                return Json(new AjaxResult { Status = "error", ErrorMsg = "用户名或密码错误" });
            }
        }

        public ActionResult CreateVarifyCode()
        {
            string varifyCode = CommonHelper.CreateVerifyCode(4);
            TempData["VarifyCode"] = varifyCode;
            MemoryStream ms = ImageFactory.GenerateImage(varifyCode, 60, 100, 20, 6);
            return File(ms, "image/jpeg");
        }

    }
}