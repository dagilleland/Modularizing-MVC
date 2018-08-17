using Microsoft.Web.UnitTestUtil;
using ModularMVC.Specs.TestData;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Xunit;
using FluentAssertions;
using System.Web.Routing;
using TestStack.BDDfy;
using System.Collections.Generic;

namespace ModularMVC.Specs
{
    public class When_I_Get_It_Right
    {
        #region String Extensions
        [Fact]
        public void Must_Create_Camel_Case()
        {
            "SomeName".ToCamelCase().Should().Be("someName");
        }
        [Fact]
        public void Must_Create_Pascal_Case()
        {
            "someName".ToPascalCase().Should().Be("SomeName");
        }
        #endregion

        /* Usage:
         *  public static void RegisterRoutes(RouteCollection routes)
         *  {
         *      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
         *      GlobalProxy.Proxy = new ProxyCore("App", "MyProxyAction");
         *      GlobalProxy.Proxy.RegisterRoute(routes);
         *  }
         */
        #region Core proxy configuration
        [Fact]
        public void Must_Identify_Name_Of_Controller_Acting_As_Proxy()
        {
            var sut = new ProxyCore("App");
            sut.ProxyController.Should().Be("App");
        }
        [Fact]
        public void Must_Identify_Proxy_Controller_Action()
        {
            var sut = new ProxyCore("App", "Proxy");
            sut.ProxyAction.Should().Be("Proxy");
        }
        [Fact]
        public void Must_Identify_Proxy_Controller_Area()
        {
            var sut = new ProxyCore("App", proxyArea: "Global");
            sut.ProxyArea.Should().Be("Global");
        }
        [Fact]
        public void Must_Use_Default_Proxy_Controller_Action()
        {
            var sut = new ProxyCore("App");
            sut.ProxyAction.Should().Be("Index");
        }
        [Fact]
        public void Must_Use_Default_Proxy_Controller_Area()
        {
            var sut = new ProxyCore("App");
            sut.ProxyArea.Should().BeNull();
        }
        [Fact]
        public void Must_Register_Route()
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            var sut = new ProxyCore("App");

            // Act
            sut.RegisterRoute(routes);

            // Assert
            routes.Should().HaveCount(1);
            routes.First().Should().BeOfType<Route>();
        }
        [Fact]
        public void Must_Generate_Correct_Route_Url()
        {
            // Arrange
            const string expectedUrl = @"App/{targetArea}/{targetController}/{targetAction}/{*params}";
            RouteCollection routes = new RouteCollection();

            // Act
            var sut = new ProxyCore("App");
            sut.RegisterRoute(routes);

            // Assert
            sut.RouteMappingUrl.Should().Be(expectedUrl);
            (routes.First() as Route).Url.Should().Be(expectedUrl);
        }
        [Fact]
        public void Must_Generate_Correct_Route_Defaults()
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            var sut = new ProxyCore("App");

            // Act
            sut.RegisterRoute(routes);

            // Assert
            (routes.First() as Route).Defaults.Should().HaveCount(3);
            const string Default_Controller_Key = "controller";
            const string Default_Action_Key = "action";
            const string Default_Area_Key = "area";
            (routes.First() as Route).Defaults.Keys.Should().Contain(Default_Controller_Key);
            (routes.First() as Route).Defaults.Keys.Should().Contain(Default_Action_Key);
            (routes.First() as Route).Defaults.Keys.Should().Contain(Default_Area_Key);
            (routes.First() as Route).Defaults[Default_Controller_Key].Should().Be(sut.ProxyController);
            (routes.First() as Route).Defaults[Default_Action_Key].Should().Be(sut.ProxyAction);
            (routes.First() as Route).Defaults[Default_Area_Key].Should().Be(sut.ProxyArea);
        }
        [Fact]
        public void Must_Generate_Correct_Route_Defaults_With_Action_And_Area()
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            var sut = new ProxyCore("MyApp", "ReRoute", "Globular");

            // Act
            sut.RegisterRoute(routes);

            // Assert
            (routes.First() as Route).Defaults.Should().HaveCount(3);
            const string Default_Controller_Key = "controller";
            const string Default_Action_Key = "action";
            const string Default_Area_Key = "area";
            (routes.First() as Route).Defaults.Keys.Should().Contain(Default_Controller_Key);
            (routes.First() as Route).Defaults.Keys.Should().Contain(Default_Action_Key);
            (routes.First() as Route).Defaults.Keys.Should().Contain(Default_Area_Key);
            (routes.First() as Route).Defaults[Default_Controller_Key].Should().Be(sut.ProxyController);
            (routes.First() as Route).Defaults[Default_Action_Key].Should().Be(sut.ProxyAction);
            (routes.First() as Route).Defaults[Default_Area_Key].Should().Be(sut.ProxyArea);
        }
        [Fact]
        public void Must_Use_Correct_RouteName()
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            var sut = new ProxyCore("App");

            // Act
            sut.RegisterRoute(routes);

            // Assert
            routes[ProxyCore.RouteName].Should().BeEquivalentTo(routes.First());
        }
        #endregion
    }
    public class When_figuring_Out_How_ActionLink_Should_Work
    {
        #region ActionLink Proxy Extension
        public static class ProxyCoreExtensions
        {
            //public static string ProjectedUrl(this ProxyCore self, string action, string controller)
            //{

            //}
        }
        [Fact]
        public void Must_Predict_Proxied_Url()
        {
            var sut = new ProxyCore("Module", "ProxyCall");
            var actual = sut.PredictProxiedUrl("Bridge", "Weapons", "FireTorpedoes");
            actual.Should().Be(@"Module/Bridge/Weapons/FireTorpedoes");

            actual = sut.PredictProxiedUrl("Bridge", "Weapons", "FireTorpedoes", "5");
            actual.Should().Be(@"Module/Bridge/Weapons/FireTorpedoes/5");

            actual = sut.PredictProxiedUrl("Bridge", "Weapons", "FireTorpedoes", "5", "Spaced", "10s");
            actual.Should().Be(@"Module/Bridge/Weapons/FireTorpedoes/5/Spaced/10s");
        }
        [Fact]
        public void Maybe()
        {
            object givenPayload = new { Power = 2500, Duration = 4000, Fade = .025 };
            var rv = new RouteValueDictionary(givenPayload);
            var actual = Flatten(rv);

            actual.Should().Be("/Power/2500/Duration/4000/Fade/0.025");
        }
        public string Flatten(RouteValueDictionary dict)
        {
            string result = "";
            var kvPairs = new List<string>();
            foreach (var pair in dict)
                kvPairs.Add($"{pair.Key}/{pair.Value}");
            result = string.Join("/", kvPairs);
            if (!string.IsNullOrWhiteSpace(result)) result = "/" + result;
            return result;
        }
        [Fact]
        public void Must_()
        {
            // Arrange
            // 1) The Mvc Module's perspective
            const string givenArea = "Shuttle";
            const string givenController = "Weapons";
            const string givenAction = "FirePhasers";
            const string givenLinkText = "Hopefully";
            object givenPayload = new { Power = 2500, Duration = 4000, Fade = .025 };
            // 2) The Proxy setup
            var sut = new ProxyCore("Module", "ProxyCall");
            // 3) Helpers
            HtmlHelper html = MvcHelper.GetHtmlHelper(rc =>
            {
                rc.Clear();
                sut.RegisterRoute(rc);
            });
            // 4) Expectations
            const string expectedUrl = @"App/{targetArea}/{targetController}/{targetAction}/{*params}";
            string expected = $"<a href=\"{MvcHelper.AppPathModifier}/app/{sut.ProxyController}/{sut.ProxyAction}/{givenArea}/{givenController}/{givenAction}\">{givenLinkText}</a>";

            // Act
            var actual = html.ActionLink(givenLinkText, sut.ProxyAction, sut.ProxyController, sut.BuildRouteValues(givenAction, givenController, givenArea, givenPayload), null);

            // Assert
            actual.ToString().Should().Be(expected);
        }






        [Fact]
        public void Must_Understand_How_It_Works()
        {
            HtmlHelper html = MvcHelper.GetHtmlHelper(rc => rc.MapRoute(null, "{area}/{controller}/{action}/{id}/{distance}", new { id = UrlParameter.Optional, distance = UrlParameter.Optional, area = "Court" }));

            const string anchor = "<a href=\"{url}\">{linkText}</a>";
            const string linkText = "Click Me";

            // ActionLink variations
            html.Encode("<br />").Should().Be("&lt;br /&gt;");
            //html.Encode(html.ActionLink(linkText, "Jump").ToString()).Should().Be(html.ActionLink(linkText, "Jump").ToString());

            this.When("I explore how it works")
                .Then(() => html.Encode(
                    html.ActionLink(linkText, "Jump")
                    .ToString())
                .Should().BeEmpty(), "ActionLink(Click Me, Jump)")
                .Then(() => html.Encode(
                    html.ActionLink(linkText, "Jump", "BasketBallPlayer")
                    .ToString())
                .Should().BeEmpty(), "ActionLink(Click Me, Jump, BasketBallPlayer)")
                .Then(() => html.Encode(
                    html.ActionLink(linkText, "Jump", "BasketBallPlayer", new { id = "Player1" }, null)
                    .ToString())
                .Should().BeEmpty(), "ActionLink(Click Me, Jump, BasketBallPlayer, { id = Player1 })")
                .Then(() => html.Encode(
                    html.ActionLink(linkText, "Jump", "BasketBallPlayer", new { distance = "High" }, null)
                    .ToString())
                .Should().BeEmpty(), "ActionLink(Click Me, Jump, BasketBallPlayer, { distance = High })")
                .Then(() => html.Encode(
                    html.ActionLink(linkText, "Jump", "BasketBallPlayer", new { id = "Player1", distance = "High" }, null)
                    .ToString())
                .Should().BeEmpty(), "ActionLink(Click Me, Jump, BasketBallPlayer, { id = Player1, distance = High })")
                .Then(() => html.Encode(
                    html.ActionLink(linkText, "Jump", "BasketBallPlayer", new { id = "Player1", distance = "High", area = "Court" }, null)
                    .ToString())
                .Should().BeEmpty(), "ActionLink(Click Me, Jump, BasketBallPlayer, { id = Player1, distance = High, area = Court })");
            this.BDDfy();
        }

        [Fact]
        public void In_Arrange_Step__Must_Include_Proxy_Route()
        {

        }
        [Fact]
        public void In_Arrange_Step__Must_Include_Mvc_Module_Route()
        {
            HtmlHelper htmlHelper_SUT = MvcHelper.GetHtmlHelper();

            htmlHelper_SUT.RouteCollection.Should().HaveCount(2);
            htmlHelper_SUT.RouteCollection.OfType<Route>().First().Url.Should().Be(@"{controller}/{action}/{id}");
            htmlHelper_SUT.RouteCollection.OfType<Route>().Last().Url.Should().Be(@"named/{controller}/{action}/{id}");

            var sut = new ProxyCore("ProxyC", "ReRoute");
            sut.RegisterRoute(htmlHelper_SUT.RouteCollection);
            htmlHelper_SUT.RouteCollection.Should().HaveCount(3);

            htmlHelper_SUT.ActionLink("click me", sut.ProxyAction, sut.ProxyController).Should().Be("Bob");
        }
        #endregion
    }
    public class When_Using_Proxy_LinkActions
    {
        private const string AppPathModifier = MvcHelper.AppPathModifier;

        [Theory]
        [MemberData(nameof(ActionLink_Data.ActionController), MemberType = typeof(ActionLink_Data))]
        public void Must_Resolve_Controller_Action(string actionName, string controllerName)
        {
            // Arrange
            string linkText = "Press Button";
            ModularMVC.GlobalProxy.Instance.Proxy =
             //null;
             "App";

            HtmlHelper htmlHelper = MvcHelper.GetHtmlHelper(controllerName, actionName);
            //FleetAreaRegistration myArea = new FleetAreaRegistration();
            //// Adapted to use ExposedObject
            //// Credits: http://igoro.com/archive/use-c-dynamic-typing-to-conveniently-access-internals-of-an-object/
            //ExposedObject.From(myArea).CreateContextAndRegister(htmlHelper.RouteCollection, null);
            //htmlHelper.RouteCollection.LowercaseUrls = true;

            // Act
            MvcHtmlString html = htmlHelper.ActionLink(GlobalProxy.Instance, linkText, actionName);//, controllerName);

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
