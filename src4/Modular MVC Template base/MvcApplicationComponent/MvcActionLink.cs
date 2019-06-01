using System;
using System.Linq;

namespace MvcApplicationComponent
{
    public class MvcActionLink
    {
        public string Area { get; set; } = string.Empty;
        public string Controller { get; set; } = "Home";
        public string Action { get; set; } = "Index";
        public string LinkText { get; set; } = "Home";
    }
}
