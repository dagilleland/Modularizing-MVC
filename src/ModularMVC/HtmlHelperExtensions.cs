using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace ModularMVC
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper self, IProxy proxy, string linkText, string actionName)
        {
            MvcHtmlString result;
            if (proxy == null || string.IsNullOrWhiteSpace(proxy.Proxy))
                result = self.ActionLink(linkText, actionName);
            else
            {
                //string controller = self.ViewContext.Controller.GetType().Name.Replace("Controller", string.Empty);
                result = self.ActionLink(linkText, actionName, null, new { Area = proxy.Proxy, action = actionName }, new RouteValueDictionary());
            }
            
            return result;
        }
        public static MvcHtmlString ActionLink(this HtmlHelper self, IProxy proxy, string linkText, string actionName, object routeValues)
        {
            var result = self.ActionLink(linkText, actionName, routeValues);

            return result;
        }
    }
}
