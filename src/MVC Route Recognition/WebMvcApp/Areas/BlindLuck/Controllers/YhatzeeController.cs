using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcApp.Areas.BlindLuck.Models;

namespace WebMvcApp.Areas.BlindLuck.Controllers
{
    public class YahtzeeController : Controller
    {
        // GET: BlindLuck/Yhatzee
        public ActionResult Index()
        {
            return View(new List<Die> { Die.New(), Die.New(), Die.New(), Die.New(), Die.New() });
        }

        public ActionResult ReRoll()
        {
            return new EmptyResult();
        }

        public ActionResult RecordResults()
        {
            return new EmptyResult();
        }

        public ActionResult Bob()
        {
            return new EmptyResult();
        }
    }
}