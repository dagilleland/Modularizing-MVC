using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ModularMVC
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper self, IProxy proxy, string linkText, string actionName, object routeValues)
        {
            var result = self.ActionLink(linkText, actionName, routeValues);
            
            return result;
        }
    }
}
