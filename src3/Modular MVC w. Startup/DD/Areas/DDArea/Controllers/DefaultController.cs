using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DD.Areas.DDArea.Controllers
{
    public class DefaultController : Controller
    {
        // GET: DDArea/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}