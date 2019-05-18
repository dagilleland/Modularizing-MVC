using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyApp.Areas.MyApp.Models
{
    public class RedirectInfo
    {
        public RedirectTarget Target { get; set; }
    }
    public class RedirectTarget
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
    }
}