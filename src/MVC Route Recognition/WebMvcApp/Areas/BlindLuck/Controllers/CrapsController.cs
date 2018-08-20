using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcApp.Areas.BlindLuck.Models;

namespace WebMvcApp.Areas.BlindLuck.Controllers
{
    public class CrapsController : Controller
    {
        // GET: BlindLuck/Craps
        public ActionResult Index()
        {
            return View(new List<Die> { Die.New(), Die.New(), Die.New(), Die.New(), Die.New() });
        }

        public ActionResult Bob()
        {
            return new EmptyResult();
        }

        public ActionResult John()
        {
            return new EmptyResult();
        }

        public ActionResult Jim()
        {
            return new EmptyResult();
        }
    }
}