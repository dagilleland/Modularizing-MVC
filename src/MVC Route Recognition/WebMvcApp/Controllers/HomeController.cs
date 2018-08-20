using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcApp.Models;

namespace WebMvcApp.Controllers
{
    public class NotAContrived : Controller
    {
        public ActionResult Index()
        {
            return new EmptyResult();
        }
    }
    //[RoutePrefix("Home")] // Annotates a controller with a route prefix that applies to all actions within the controller
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        //[Route("Index")]
        public ActionResult Index()
        {
            return View(new SampleRouteInfo());
        }

        [HttpPost]
        //[Route("Index/{data:SampleRouteInfo}")]
        public ActionResult Index(SampleRouteInfo data)
        {
            return View(data);
        }

        public ActionResult Me(int number, string name)
        {
            return View();
        }
        public ActionResult You(string fullName, int num)
        {
            return View();
        }
    }
}