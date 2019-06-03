using ProjName_HostApp.Backend;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjName_HostApp.Controllers
{
    public class DatabaseController : Controller
    {
        #region Initialization
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            App_Data_Path = Server.MapPath("~/App_Data/");
        }
        private string App_Data_Path;
        #endregion

        #region DatabaseManager Jobs
        public ActionResult Index()
        {
            DatabaseConnection conn = new DbMasterContext().DatabaseConnection;
            return View(conn);
        }

        public ActionResult Create()
        {
            new DbMasterContext().CreateDatabase();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete()
        {
            new DbMasterContext().DeleteDatabase();
            return RedirectToAction(nameof(Index));
        }

        [ChildActionOnly]
        public ActionResult Backup(string backupSuffix)
        {
            if(!string.IsNullOrWhiteSpace(backupSuffix))
                new DbMasterContext().Backup(backupSuffix);
            ViewBag.BackupResult = backupSuffix;
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Restore(string restoreSuffix)
        {
            if (!string.IsNullOrWhiteSpace(restoreSuffix))
                new DbMasterContext().Restore(restoreSuffix);
            ViewBag.RestoreResult = restoreSuffix;
            return PartialView();
        }
        #endregion

        #region DacPacManager Jobs
        [ChildActionOnly]
        public ActionResult ListDacPacFiles()
        {
            List<DacPacFileInfo> result = new List<DacPacFileInfo>();
            result = DatabaseManager.ListDacPacs(App_Data_Path);
            return PartialView(result);
        }

        [ChildActionOnly]
        public ActionResult DeployDacPac(string dacPacName)
        {
            // TODO: 
            List<string> results = new List<string>();
            if (!string.IsNullOrWhiteSpace(dacPacName))
            {
                var info = new DbMasterContext().DatabaseConnection;
                results = new DacPacManager(App_Data_Path).ProcessDacPac(ConfigurationManager.ConnectionStrings[info.ConnectionStringName].ConnectionString, info.InitialCatalog, dacPacName);
                    }
            return PartialView(results);
        }

        [ChildActionOnly]
        public ActionResult GetDeploymentPlan(string dacPac)
        {
            // TODO: 
            List<string> results = new List<string>();
            if (!string.IsNullOrWhiteSpace(dacPac))
            {
                var info = new DbMasterContext().DatabaseConnection;
                results = new DacPacManager(App_Data_Path).GetDeploymentPlan(ConfigurationManager.ConnectionStrings[info.ConnectionStringName].ConnectionString, info.InitialCatalog, dacPac);
            }
            return PartialView(results);
        }
        #endregion
    }
}