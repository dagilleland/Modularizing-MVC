using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EE.Areas.EEArea.Controllers
{
    public class DefaultController : Controller
    {
        // GET: EEArea/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}