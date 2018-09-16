using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.CommonMVC;
using ZSZ.IService;

namespace ZSZ.AdminWeb.App_Start
{
    public class ZSZAuthrizationFilter:IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
             CheckPermissionAttribute[] perAttrs =  (CheckPermissionAttribute[])filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckPermissionAttribute),false);
            if (perAttrs.Length <= 0)
            {
                return;
            }
            long? userId = (long?)filterContext.HttpContext.Session["AdminUserId"];
            if (userId == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    AjaxResult ajaxResult = new AjaxResult();
                    ajaxResult.Status = "redirect";
                    ajaxResult.Data = "/Main/Login";
                    ajaxResult.ErrorMsg = "请登录";
                    filterContext.Result = new JsonNetResult() {Data = ajaxResult};
                    return;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Main/Login");
                    return;
                }
                
            }
            
            IAdminUserService adminUserServ = (IAdminUserService)DependencyResolver.Current.GetService(typeof (IAdminUserService));
            foreach (var perAttr in perAttrs)
            {
                if (!adminUserServ.HasPermission((long)userId, perAttr.Permission))
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        AjaxResult ajaxResult = new AjaxResult();
                        ajaxResult.Status = "error";
                        ajaxResult.ErrorMsg = $"没有{perAttr.Permission}权限";
                        filterContext.Result = new JsonNetResult() {Data = ajaxResult};
                        return;
                    }
                    else
                    {
                        filterContext.Result = new ContentResult() { Content = $"没有{perAttr.Permission}权限" };
                        return;
                    }
                    
                }
            }
        }
    }
}