using Microsoft.Web.UnitTestUtil;
using ModularMVC.Specs.TestData;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Xunit;

namespace ModularMVC.Specs
{
    public class When_Using_Proxy_LinkActions
    {
        private const string AppPathModifier = MvcHelper.AppPathModifier;

        [Theory]
        [MemberData(nameof(ActionLink_Data.ActionController), MemberType = typeof(ActionLink_Data))]
        public void Must_Resolve_Controller_Action(string actionName, string controllerName)
        {
            // Arrange
            string linkText = "Press Button";
            HtmlHelper htmlHelper = MvcHelper.GetHtmlHelper();
            //FleetAreaRegistration myArea = new FleetAreaRegistration();
            //// Adapted to use ExposedObject
            //// Credits: http://igoro.com/archive/use-c-dynamic-typing-to-conveniently-access-internals-of-an-object/
            //ExposedObject.From(myArea).CreateContextAndRegister(htmlHelper.RouteCollection, null);
            //htmlHelper.RouteCollection.LowercaseUrls = true;

            // Act
            MvcHtmlString html = htmlHelper.ActionLink(linkText, actionName, controllerName);

            // Assert
            Assert.Equal($@"<a href=""{AppPathModifier}/app/{controllerName}/{actionName}"">{linkText}</a>", html.ToHtmlString());
        }
        // Class for the Area related tests
        private class FleetAreaRegistration : AreaRegistration
        {
            private const string Fleet = nameof(Fleet);
            public override string AreaName
            {
                get
                {
                    return Fleet;
                }
            }

            public override void RegisterArea(AreaRegistrationContext context)
            {
                context.MapRoute(
                    $"{Fleet}_default",
                    $"{Fleet}/{{controller}}/{{action}}/{{id}}",
                    new { action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    }
}
