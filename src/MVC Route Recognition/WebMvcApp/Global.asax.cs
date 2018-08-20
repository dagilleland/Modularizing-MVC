using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using WebMvcApp.Models;

namespace WebMvcApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IReadOnlyList<MvcModule> RegisteredMvcModules { get; private set; }
        public static List<Type> RelatedControllers { get; private set; }

        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisteredMvcModules = RouteTable.Routes.GetRegisteredModules();
            RelatedControllers = new List<Type>();
            foreach (var registration in RegisteredMvcModules)
            {
                registration.FindActions(AppDomain.CurrentDomain.GetAssemblies());
            }

        }
    }
}
