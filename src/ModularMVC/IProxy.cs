using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ModularMVC
{
    public static class StringExtras
    {
        /// <summary>
        /// Camel Case is...
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string self)
        {
            if (string.IsNullOrWhiteSpace(self))
                return self;
            else if (self.Length == 1)
                return self.ToLower();
            else
                return char.ToLower(self[0]) + self.Substring(1);
        }
        /// <summary>
        /// Pascal Case is a version of Camel Case where the first letter is capitalized.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string self)
        {
            if (string.IsNullOrWhiteSpace(self))
                return self;
            else if (self.Length == 1)
                return self.ToUpper();
            else
                return char.ToUpper(self[0]) + self.Substring(1);
        }
    }
    /// <summary>
    /// The CLASSNAME provides a pre-configured routing and action pattern where the specified proxy controller's action has the parameters of (string TargetArea, string TargetController, string TargetAction, string Id). It also exposes key information on the proxy for use in the @Html.ActionLink() helper extension.
    /// </summary>
    public class ProxyCore
    {
        #region String constructs (static and const)
        private const string ModularMvcHostProxy = nameof(ModularMvcHostProxy);
        private const string Index = nameof(Index);
        private const string TargetArea = nameof(TargetArea);
        private const string TargetController = nameof(TargetController);
        private const string TargetAction = nameof(TargetAction);

        private static string PlaceHolder_TargetArea => Placeholder(nameof(TargetArea));
        private static string PlaceHolder_TargetController => Placeholder(nameof(TargetController));
        private static string PlaceHolder_TargetAction => Placeholder(nameof(TargetAction));

        public static string RouteName => ModularMvcHostProxy;
        private static string Placeholder(string name) => "{" + name.ToCamelCase() + "}";
        #endregion

        public readonly string ProxyController;
        public readonly string ProxyAction;
        public readonly string ProxyArea;

        public ProxyCore(string proxyController, string proxyAction = Index, string proxyArea = null)
        {
            if (string.IsNullOrEmpty(proxyController))
            {
                throw new ArgumentException($"{nameof(proxyController)} is null or empty.", nameof(proxyController));
            }

            if (string.IsNullOrEmpty(proxyAction))
            {
                throw new ArgumentException($"{nameof(proxyAction)} is null or empty.", nameof(proxyAction));
            }

            ProxyController = proxyController;
            ProxyAction = proxyAction;
            ProxyArea = proxyArea;
        }
        private string OptionalProxyAreaMap => string.IsNullOrWhiteSpace(ProxyArea) ? string.Empty : ProxyArea + "/";
        public string RouteMappingUrl => $@"{OptionalProxyAreaMap}{ProxyController}/{ProxyAction}/{PlaceHolder_TargetArea}/{PlaceHolder_TargetController}/{PlaceHolder_TargetAction}/{{*payload}}";
        private object RouteMappingDefaults => new { controller = ProxyController, action = ProxyAction, area = ProxyArea, payload = UrlParameter.Optional };
        public void RegisterRoute(RouteCollection routes)
        {
            routes.MapRoute(RouteName, RouteMappingUrl, RouteMappingDefaults);
        }

        public RouteValueDictionary BuildRouteValues(string targetAction, string targetController, string targetArea, object targetPayload = null)
        {
            var dict = new RouteValueDictionary();
            dict.Add("controller", ProxyController);
            dict.Add("action", ProxyAction);
            dict.Add("area", ProxyArea);

            dict.Add(TargetAction.ToCamelCase(), targetAction);
            dict.Add(TargetController.ToCamelCase(), targetController);
            dict.Add(TargetArea.ToCamelCase(), targetArea);
            if (targetPayload != null)
                dict.Add("payload", Flatten(targetPayload));
            //dict.Add("payload", targetPayload);
            return dict;
        }
        private string Flatten(object obj)
        {
            RouteValueDictionary dict = new RouteValueDictionary(obj);
            string result = "";
            var kvPairs = new List<string>();
            foreach (var pair in dict)
                kvPairs.Add($"{pair.Key}/{pair.Value}");
            result = string.Join("/", kvPairs);
            //if (!string.IsNullOrWhiteSpace(result)) result = "/" + result;
            return result;
        }


        #region For Testing
        // All are assuming correct routing has been set up.
        public string PredictProxiedUrl(string area, string controller, string action)
        {
            return RouteMappingUrl.Replace(PlaceHolder_TargetController, controller).Replace(PlaceHolder_TargetAction, action).Replace(PlaceHolder_TargetArea, area).Replace(@"/{*payload}", string.Empty);
        }
        public string PredictProxiedUrl(string area, string controller, string action, params string[] keyValuePairs)
        {
            return RouteMappingUrl.Replace(PlaceHolder_TargetController, controller).Replace(PlaceHolder_TargetAction, action).Replace(PlaceHolder_TargetArea, area).Replace(@"{*payload}", string.Join("/", keyValuePairs));
        }

        #endregion
    }

    public interface IProxy
    {
        string Proxy { get; set; }
    }
    public class Proxy : RouteValueDictionary
    {
        public string ProxyController { get; set; }
        public string ProxyAction { get; set; }
        public string TargetController { get; set; }
        public string TargetAction { get; set; }
        public string TargetArea { get; set; }
        public string Payload { get; set; }

        /*
@Url.Action() --> /Home/Index 
@Url.Action("Index") --> / 
@Url.Action("About") --> /Home/About 
@Url.Action("Index", "App") --> /App 
@Url.Action("Index", "App", new { controller = "App", action = "Index", area = "Fleet" }) --> /Fleet/App 
@Url.Action("Index", "App", new { controller = "App", action = "Index", areaName = "Fleet", ctr = "Uncle", act = "Bob", payload = "Martin" }) --> /App/Fleet/Uncle/Bob/Martin 
@Url.Action(null, null, new { controller = "App", action = "Index", areaName = "Fleet", ctr = "Uncle", act = "Bob", payload = "Martin" }) --> /App/Fleet/Uncle/Bob/Martin 
         * 
         * Desired Results:
@Html.ActionLink(GlobalProxy.Instance, "Edit Me", "Edit", new { id=item.PrimaryKey  })
 x   <a href="/App/Edit/5">Edit Me</a>


\\\\\\\\\\\\\\\\
@Html.ActionLink(GlobalProxy.Instance, "Edit Me", "Edit", "Ships", new { area = "Fleet" })
    <a href="/App/Fleet/Ships/Edit">Edit Me</a>
/////////////


\\\\\\\\\\\\\\\\
public class RouteInfo
{
    public string AreaName {get;set;}
    public string ControllerName {get;set;}
    public string ActionName {get;set;}
    public object RouteData {get;set;}
    public RouteInfo For(string action)
    {
        ActionName = action;
        return this;
    }
    public RouteInfo By(string controller)
    {
        ControllerName = controller;
        return this;
    }
    public RouteInfo With(object data)
    {
        RouteData = data;
        return this;
    }
}
public static class ControllerRouteExtensions
{
    public static RouteInfo RouteInfo(this Controller self, string area = null, string action = "Index")
    {
        return new RouteInfo
        {
            AreaName = area,
            ControllerName = self.GetType().Name.Replace("Controller", string.Empty),
            ActionName = action
        };
    }
}
public class ShipsController : Controller
{
    // Route info with a default
    private RouteInfo Info = this.RouteInfo("Fleet");

    public void Index()
    {
        ViewBag.CreateRoute = Info;
        // ...
        return View();
    }

    public void Index()
    {
        // ...
        return View();
    }

    public void Create()
    {
        ViewBag.RouteInfo = this.RouteInfo("Fleet");
        return View();
    }
    [HttpPost]
    public void Create(Ship newShip)
    {
        ViewBag.RouteInfo = this.RouteInfo("Fleet");
        // process newShip...
        return View(newShip);
    }
}
...
@Html.ActionLink(GlobalProxy.Instance, "Cancel Create Ship", ViewBag.RouteInfo)
@Html.ActionLink(GlobalProxy.Instance, "

@Html.ActionLink(GlobalProxy.Instance, "Edit Me", "Edit", "Ships", new { area = "Fleet" })
    <a href="/App/Fleet/Ships/Edit">Edit Me</a>
/////////////

@Html.ActionLink(GlobalProxy.Instance, "Edit Me", "Edit", new { id=item.PrimaryKey  })
    <a href="/App/Edit/5">Edit Me</a>

@Html.ActionLink(GlobalProxy.Instance, "Edit Me", "Edit", new { id=item.PrimaryKey  })
    <a href="/App/Edit/5">Edit Me</a>

@Html.ActionLink(GlobalProxy.Instance, "Edit Me", "Edit", new { id=item.PrimaryKey  })
    <a href="/App/Edit/5">Edit Me</a>

         */

    }
}
