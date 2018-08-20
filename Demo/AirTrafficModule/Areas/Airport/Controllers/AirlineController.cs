using AirTrafficModule.Areas.Airport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirTrafficModule.Areas.Airport.Controllers
{
    public class AirlineController : Controller
    {
        // GET: Airport/Airline
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email)
        {
            return View(new BookingContact { Email = email, SubmitteDateTime = DateTime.Now });
        }
    }
}