using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IService;

namespace ZSZ.FrontWeb.Controllers
{
    public class MainController : Controller
    {
        public ICityService CityService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
    }
}