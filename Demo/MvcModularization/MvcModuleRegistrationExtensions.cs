using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcModularization
{
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
}
