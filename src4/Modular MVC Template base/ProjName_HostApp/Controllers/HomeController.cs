using ProjName_HostApp.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjName_HostApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private List<DacPacFileInfo> PrepDbInfo()
        {
            List<DacPacFileInfo> result = new List<DacPacFileInfo>();
            ViewBag.IsInstalled = DatabaseManager.CheckDatabaseConnection();
            if (!ViewBag.IsInstalled)
            {
                result = DatabaseManager.ListDacPacs(Server.MapPath("~/App_Data/"));
            }
            return result;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View(PrepDbInfo());
        }

        public ActionResult InstallDb()
        {
            DatabaseManager.PublishDacPacs(Server.MapPath("~/App_Data/"));

            return View(nameof(About), PrepDbInfo());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}