using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var log = CommonStartupCode.FileLog.ReadLog(Server.MapPath("~"));
            ViewBag.FileLog = log;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}