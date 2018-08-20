using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MvcModularization;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IReadOnlyList<MvcModule> RegisteredMvcModules { get; private set; }
        void FinalizeMvcModuleRegistration(RouteCollection routes)
        {
            RegisteredMvcModules = routes.GetRegisteredModules();
            foreach (var registration in RegisteredMvcModules)
            {
                registration.FindActions(AppDomain.CurrentDomain.GetAssemblies());
            }
        }

        protected void Application_Start()
        {
            RouteTable.Routes.MapMvcAttributeRoutes();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FinalizeMvcModuleRegistration(RouteTable.Routes);
        }
    }
}
