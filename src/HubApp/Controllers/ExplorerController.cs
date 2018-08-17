using HubApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HubApp.Controllers
{
    public class ExplorerController : Controller
    {
        // GET: Explorer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            var obj = new Exploration { Id = id };
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            ViewBag.Json = json;
            return View("Index", obj);
        }
    }
}