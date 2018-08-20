using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using WebMvcApp.Models;

namespace WebMvcApp
{
    #region Mvc Module Registration package
    /// <summary>
    /// 
    /// </summary>
    public static class MvcModuleRegistrationExtensions
    {
        #region MvcModule Registration on the DataTokens collection
        public static IReadOnlyList<MvcModule> GetRegisteredModules(this RouteCollection collection)
        {
            var results = new List<MvcModule>();
            using (collection.GetReadLock()) // A little thread safety..
            {
                foreach (var route in collection?.OfType<Route>())
                {
                    var module = route.GetModuleRegistration();
                    if (module != null) results.Add(module.Clone());
                }
            }
            return results.AsReadOnly();
        }

        public const string __RegistrationKey = nameof(__RegistrationKey);
        public static MvcModule GetModuleRegistration(this Route route)
        {
            if (route == null) return null;
            return route.DataTokens.GetModuleRegistration();
        }
        public static MvcModule GetModuleRegistration(this RouteData routeData)
        {
            if (routeData == null) return null;
            return routeData.DataTokens.GetModuleRegistration();
        }
        public static MvcModule GetModuleRegistration(this RouteValueDictionary routeValues)
        {
            if (routeValues == null) return null;

            object routeName = null;
            routeValues.TryGetValue(__RegistrationKey, out routeName);
            return routeName as MvcModule;
        }
        public static Route SetModuleRegistration(this Route route, MvcModule registration, AreaRegistrationContext context)
        {
            if (route == null)
                throw new ArgumentNullException(nameof(route));
            if (registration == null)
                throw new ArgumentNullException(nameof(registration));

            if (route.DataTokens == null)
            {
                route.DataTokens = new RouteValueDictionary();
            }
            var clone = registration.Clone();
            clone.AddNamespaces(context.Namespaces);
            route.DataTokens[__RegistrationKey] = clone;
            return route;
        }
        #endregion

        #region Phil Haack's code from "Getting The Route Name For A Route"
        // see https://haacked.com/archive/2010/11/28/getting-the-route-name-for-a-route.aspx/
        public static string GetRouteName(this Route route)
        {
            if (route == null) return null;
            return route.DataTokens?.GetRouteName();
        }

        public static string GetRouteName(this RouteData routeData)
        {
            if (routeData == null) return null;
            return routeData.DataTokens.GetRouteName();
        }

        public static string GetRouteName(this RouteValueDictionary routeValues)
        {
            if (routeValues == null) return null;
            object routeName = null;
            routeValues.TryGetValue("__RouteName", out routeName);
            return routeName as string;
        }

        public static Route SetRouteName(this Route route, string routeName)
        {
            if (route == null)
            {
                throw new ArgumentNullException("route");
            }
            if (route.DataTokens == null)
            {
                route.DataTokens = new RouteValueDictionary();
            }
            route.DataTokens["__RouteName"] = routeName;
            return route;
        }
        #endregion
    }
    public interface IRouteHome
    {
        string DefaultController { get; }
        string DefaultAction { get; }
        string DefaultArea { get; }
        string DefaultLinkText { get; }
        ICollection<string> Namespaces { get; }
    }
    public sealed class MvcModule : IRouteHome
    {
        public string DefaultController { get; private set; }
        public string DefaultAction { get; private set; }
        public string DefaultArea { get; private set; }
        public string DefaultLinkText { get; private set; }
        public ICollection<string> Namespaces { get; private set; } = new List<string>();
        public MvcModule(string defaultController, string defaultAction, string defaultArea, string defaultLinkText)
        {
            if (string.IsNullOrEmpty(defaultController))
                throw new ArgumentException($"{nameof(defaultController)} is null or empty.", nameof(defaultController));
            if (string.IsNullOrEmpty(defaultAction))
                throw new ArgumentException($"{nameof(defaultAction)} is null or empty.", nameof(defaultAction));
            if (string.IsNullOrEmpty(defaultArea))
                throw new ArgumentException($"{nameof(defaultArea)} is null or empty.", nameof(defaultArea));
            if (string.IsNullOrEmpty(defaultLinkText))
                throw new ArgumentException($"{nameof(defaultLinkText)} is null or empty.", nameof(defaultLinkText));

            DefaultController = defaultController;
            DefaultAction = defaultAction;
            DefaultArea = defaultArea;
            DefaultLinkText = defaultLinkText;
        }

        internal MvcModule Clone() { return this.MemberwiseClone() as MvcModule; }
        internal void AddNamespaces(ICollection<string> namespaces)
        {
            if (namespaces == null) throw new ArgumentNullException(nameof(namespaces));
            foreach (var item in namespaces)
                Namespaces.Add(item);
        }
        public void FindActions(Assembly[] assemblies)
        {
            foreach (var ns in Namespaces)
            {
                string searchArea = ns;
                if (searchArea.EndsWith(".*")) searchArea = searchArea.Substring(0, searchArea.Length - 2);
                // FIND the controller class!!
                foreach (var assm in assemblies)
                {
                    try
                    {
                        var foundControllers = assm
                            .DefinedTypes
                            //.GetTypes()
                            .Where(x => x.IsClass && x.IsSubclassOf(typeof(Controller)) &&
                            x.Namespace.StartsWith(searchArea));
                        //x.FullName == $"{searchArea}.{registration.DefaultController}Controller");
                        foreach (var controller in foundControllers)
                        {
                            AvailableControllers.Add(controller.FullName, new AvailableActions());
                            var methods = controller.GetMethods(System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                            foreach (var action in methods)
                                AvailableControllers[controller.FullName].Add(action.Name, action);
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }

        }

        public AvailableControllers AvailableControllers { get; private set; } = new AvailableControllers();
    }
    public class AvailableControllers : Dictionary<string, AvailableActions>
    {
    }
    public class AvailableActions : Dictionary<string, MemberInfo>
    {
    }
    #endregion

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            DefaultInlineConstraintResolver resolver = new DefaultInlineConstraintResolver();
            resolver.ConstraintMap.Add("SampleRouteInfo", typeof(SampleRouteInfo));
            RouteTable.Routes.MapMvcAttributeRoutes(resolver);

            AreaRegistration.RegisterAllAreas();

            //routes.MapRoute(
            //    name: "App_Proxy",
            //    url: "App/{nameOfArea}/{nameOfController}/{nameOfAction}/{*payload}",
            //    defaults: new { controller = "App", action = "Proxy", payload = UrlParameter.Optional },
            //    constraints: new { controller = "App" }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, isHub = true, main = new { } }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    constraints: new { controller = "Home|Services|Contact|Error" }
            //);

            routes.MapRoute(
                name: "catchall",
                //url: "{*url}",
                url: "Error/{*url}",
                defaults: new { controller = "Error", action = "NotFound" }
            );



            routes.MapRoute(
                name: "NotReal",
                //url: "{*url}",
                url: "Flight/To/The/Stars",
                defaults: new { controller = "Error", action = "NotFound" }
            );

        }
    }
}
