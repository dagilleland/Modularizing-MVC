using System;
using System.Linq;
using System.Web.Routing;

namespace ModularMVC
{
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
