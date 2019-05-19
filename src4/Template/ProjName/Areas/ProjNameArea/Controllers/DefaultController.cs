﻿using $safeprojectname$.Areas.$safeprojectname$Area.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace $safeprojectname$.Areas.$safeprojectname$Area.Controllers
{
    public class DefaultController : Controller
    {
        private const string BaseRef = "/Areas/$safeprojectname$Area";
        internal static List<DemoModule> Presets = new List<DemoModule>();
        internal static string HostAppName { get; set; }

        public ActionResult Index()
        {
            ViewBag.BaseRef = BaseRef;
            var data = Presets;
            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.BaseRef = BaseRef;
            ViewBag.HostName = HostAppName;
            return View();
        }

        public ActionResult SampleBase()
        {
            ViewBag.BaseRef = BaseRef;
            var data = Presets.FirstOrDefault();
            return View(data);
        }
    }
}