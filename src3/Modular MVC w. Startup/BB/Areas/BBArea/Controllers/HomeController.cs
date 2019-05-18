using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB.Areas.BBArea.Controllers
{
    public class HomeController : Controller
    {
        // GET: BBArea/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}