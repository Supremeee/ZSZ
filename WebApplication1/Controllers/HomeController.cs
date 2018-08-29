using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIService;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IUserService UserService{ get; set; }
        // GET: Home
        public ActionResult Index()
        {
            bool b = UserService.CheckLogin("1 ", "1");
            return Content(b.ToString());
        }
        public ActionResult Page()
        {
            return View();
        }
    }
}