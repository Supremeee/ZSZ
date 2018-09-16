using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                filterContext.Result = new ContentResult() {Content = "没有登录"};
                return;
            }
            
            IAdminUserService adminUserServ = (IAdminUserService)DependencyResolver.Current.GetService(typeof (IAdminUserService));
            foreach (var perAttr in perAttrs)
            {
                if (!adminUserServ.HasPermission((long)userId, perAttr.Permission))
                {
                    filterContext.Result= new ContentResult() {Content = $"没有{perAttr.Permission}权限"};
                    return;
                }
            }
        }
    }
}