using AirTrafficModule.Areas.Airport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AirTrafficModule.Areas.Airport.Models.RunwayClearance;

namespace AirTrafficModule.Areas.Airport.Controllers
{
    public class AirTrafficController : Controller
    {
        // GET: Airport/AirTraffic
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? flight, string runway, Purpose? clearance)
        {
            RunwayClearance obj = null;
            if (flight.HasValue && !string.IsNullOrWhiteSpace(runway) && clearance.HasValue)
                obj = new RunwayClearance
                {
                    FlightNumber = flight.Value,
                    Runway = runway,
                    Reason = clearance.Value
                };
            else
                ViewBag.Noise = "Did you just hear a noise?";
            return View(obj);
        }
    }
}