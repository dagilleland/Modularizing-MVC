using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace WebApp.Models
{
    public class SampleRouteInfo
    {
        public string NameOfArea { get; set; }
        public string NameOfController { get; set; }
        public string NameOfAction { get; set; }

        public string Id { get; set; }

        public Dictionary<string, string> OtherValues { get; set; } =
            new Dictionary<string, string>();
    }
}