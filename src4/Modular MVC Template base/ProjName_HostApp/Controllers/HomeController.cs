using ProjName_HostApp.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
            DatabaseManager.PublishDacPacs(App_Data_Path);

            return View(nameof(About), PrepDbInfo());
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            App_Data_Path = Server.MapPath("~/App_Data/");
        }
        private string App_Data_Path;
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            var apps = DatabaseManager.ReadInstallationLog(App_Data_Path);
            if (apps.Count == 0)
            {
                DatabaseManager.RegisterHostApp(App_Data_Path, new MvcActionLink { Controller = "Home", LinkText = "Host Application" });
            }
            return View(apps);
        }
    }
}