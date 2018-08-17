using HubApp.Areas.StarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HubApp.Areas.StarSystem.Controllers
{
    public class PlanetController : Controller
    {
        // GET: StarSystem/Planet
        public ActionResult Index()
        {
            ViewBag.RouteInfo = this.RouteData.Values;
            ViewBag.Tokens = this.RouteData.DataTokens;
            return View(SolSystem.Planets.Values);
        }
    }
}