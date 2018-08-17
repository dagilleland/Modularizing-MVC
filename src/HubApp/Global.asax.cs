using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HubApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //ModularMVC.GlobalProxy.Instance.Proxy = "App";
            RouteTable.Routes.MapMvcAttributeRoutes();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcApplication.RouteTableRoutes = RouteTable.Routes;
        }
        public static RouteCollection RouteTableRoutes { get; set; }
    }
}
