using Microsoft.Web.UnitTestUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;
using System.Web.Mvc.Html;
using System.Dynamic;
using IgorO.ExposedObjectProject;
using Xunit.Extensions;
using ModularMVC.Specs.TestData;

namespace ModularMVC.Specs
{
    // CLEAN: Used for educational purposes only - delete later
    /// <summary>
    /// Part of LinkExtensionsTest from https://github.com/aspnet/AspNetWebStack/blob/master/test/System.Web.Mvc.Test/Html/Test/LinkExtensionsTest.cs
    /// </summary>
    public class Understanding_LinkAction
    {
        private const string AppPathModifier = MvcHelper.AppPathModifier;

        [Fact]
        public void ActionLink()
        {
            // Arrange
            HtmlHelper htmlHelper = MvcHelper.GetHtmlHelper();

            // Act
            MvcHtmlString html = htmlHelper.ActionLink("linktext", "newaction");

            // Assert
            Assert.Equal(@"<a href=""" + AppPathModifier + @"/app/home/newaction"">linktext</a>", html.ToHtmlString());
        }

        [Fact]
        public void ActionLinkProducesLowercaseUrlsAfterRegisteringAnArea()
        {
            // Arrange
            HtmlHelper htmlHelper = MvcHelper.GetHtmlHelper();
            MyAreaRegistration myArea = new MyAreaRegistration();
            // Adapted to use ExposedObject
            // Credits: http://igoro.com/archive/use-c-dynamic-typing-to-conveniently-access-internals-of-an-object/
            ExposedObject.From(myArea).CreateContextAndRegister(htmlHelper.RouteCollection, null);
            htmlHelper.RouteCollection.LowercaseUrls = true;

            // Act
            MvcHtmlString html = htmlHelper.ActionLink("about", "About", "Home");

            // Assert
            Assert.True(html.ToHtmlString() == html.ToHtmlString().ToLowerInvariant());
        }

        // Class for the ActionLinkProducesLowercaseUrlsAfterRegisteringAnArea test
        private class MyAreaRegistration : AreaRegistration
        {
            public override string AreaName
            {
                get
                {
                    return "MyArea";
                }
            }

            public override void RegisterArea(AreaRegistrationContext context)
            {
                context.MapRoute(
                    "MyArea_default",
                    "MyArea/{controller}/{action}/{id}",
                    new { action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    }
}
