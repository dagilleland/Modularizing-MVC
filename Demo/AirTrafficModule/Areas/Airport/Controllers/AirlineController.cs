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
        public ActionResult Index()
        {
            return View();
        }
    }
}