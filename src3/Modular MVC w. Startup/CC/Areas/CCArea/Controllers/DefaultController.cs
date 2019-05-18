using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CC.Areas.CCArea.Controllers
{
    public class DefaultController : Controller
    {
        // GET: CCArea/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}