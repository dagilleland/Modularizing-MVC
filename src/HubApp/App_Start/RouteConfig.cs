using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HubApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Proxy route for MVC modules
            //routes.MapRoute(
            //    name: "ProxyDefault",
            //    url: "App",
            //    defaults: new { controller = "App", action = "Index" }
            //);
            routes.MapRoute(
                name: "Proxy",
                url: "App/{areaName}/{ctr}/{act}/{payload}",
                defaults: new { controller = "App", action = "Index", areaName = UrlParameter.Optional, ctr = UrlParameter.Optional, act = UrlParameter.Optional, payload = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Proxy",
            //    url: "App/{payload}",
            //    defaults: new { controller = "App", action = "Index", payload = UrlParameter.Optional }
            //);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
