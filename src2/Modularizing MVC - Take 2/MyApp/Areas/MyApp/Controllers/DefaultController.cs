using MyApp.Areas.MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyApp.Areas.MyApp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: MyApp/Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View(new RedirectTarget { Action = "Info", Controller = "Default", Area = "MyApp" });
        }

        public ActionResult GoTo(RedirectInfo info)
        {
            if (info != null && info.Target != null)
                return RedirectToAction(info.Target.Action, new { Controller = info.Target.Controller, Area = info.Target.Area });
            else
                return Redirect("~");
        }
    }
}