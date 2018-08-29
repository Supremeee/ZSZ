using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using System.Reflection;
using ModelBinder;
using ZSZ.IService;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new JsonNetActionFilter());
            ModelBinders.Binders.Add(typeof(string), new TrimToDBCModelBinder()); 
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();//把当前程序集中的所有的Controllers都注册

            Assembly asmService = Assembly.Load("TestService");
            builder.RegisterAssemblyTypes(asmService).Where(e => !e.IsAbstract&& typeof(IServiceSupport).IsAssignableFrom(e)).AsImplementedInterfaces().PropertiesAutowired();//最后的一个负责属性注入
            //type1.IsAssignableFrom(type2) 表示 type1是否可以指向type2 或者说 type2是够实现了type1的接口/继承了type2
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//注册系统级别的DependencyResolver 使MVC框架在创建Controller使都是由Autofac来创建对象。因为Controller等对象是由Autofac创建的，所以他用到了BLL层的属性也会有Autofac自动创建
        }
    }
}
