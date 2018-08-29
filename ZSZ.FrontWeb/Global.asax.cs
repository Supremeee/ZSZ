using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZSZ.CommonMVC;
using ZSZ.FrontWeb.App_Start;
using ZSZ.IService;

namespace ZSZ.FrontWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(int), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(long), new TrimToDBCModelBinder());
            ModelBinders.Binders.Add(typeof(double), new TrimToDBCModelBinder());

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前程序集中的所有的Controllers都注册

            Assembly asmService = Assembly.Load("ZSZ.Service");
            builder.RegisterAssemblyTypes(asmService).Where(e => !e.IsAbstract && typeof(IServiceSupport).IsAssignableFrom(e)).AsImplementedInterfaces().PropertiesAutowired();//最后的一个负责属性注入
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//注册系统级别的DependencyResolver 使MVC框架在创建Controller使都是由Autofac来创建对象。因为Controller等对象是由Autofac创建的，所以他用到了BLL层的属性也会有Autofac自动创建

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new ZSZExceptionFilter());
        }
    }
}
