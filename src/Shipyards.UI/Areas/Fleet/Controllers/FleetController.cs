using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shipyards.UI.Areas.Fleet.Controllers
{
    public class FleetController : Controller
    {
        // GET: Fleet/Fleet
        public ActionResult Index()
        {
            return View();
        }
    }
}