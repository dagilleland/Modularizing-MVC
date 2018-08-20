using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WebMvcApp.Models
{
    public class SampleRouteInfo : IRouteConstraint
    {
        public string NameOfArea { get; set; }
        public string NameOfController { get; set; }
        public string NameOfAction { get; set; }

        public string Id { get; set; }

        public Dictionary<string, string> OtherValues { get; set; } =
            new Dictionary<string, string>();

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            throw new NotImplementedException();
        }
    }
}