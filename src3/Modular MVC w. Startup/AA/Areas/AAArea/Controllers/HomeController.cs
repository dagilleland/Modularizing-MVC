using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AA.Areas.AAArea.Controllers
{
    public class HomeController : Controller
    {
        // GET: AAArea/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}