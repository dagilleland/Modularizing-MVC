using HubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HubApp.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index(string area, string ctr, string act)
        {
            AreaProxy obj;
            if (string.IsNullOrEmpty(area + ctr + act))
                obj = null;
            else
            obj =
                new AreaProxy
                {
                    AreaName = area,
                    ControllerName = ctr,
                    ActionName = act
                };

            return View(obj);
        }

        // GET: App
        //public ActionResult Index(AreaProxy payload)
        //{
        //    AreaProxy obj;
        //        obj =
        //            payload;

        //    return View(obj);
        //}

        public ActionResult Proxy(AreaProxy obj)
        {
            //var obj = new AreaProxy
            //{
            //    Area = Area,
            //    Controller = Controller,
            //    ActionName = Action,
            //    Payload = Payload
            //};

            return View("Index", obj);
        }
    }
}