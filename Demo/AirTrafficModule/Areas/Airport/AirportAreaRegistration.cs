using System.Web.Mvc;
using MvcModularization;

namespace AirTrafficModule.Areas.Airport
{
    public class AirportAreaRegistration : AreaRegistration 
    {
        private const string Airport = nameof(Airport);
        private const string AirTraffic = nameof(AirTraffic);
        private const string Index = nameof(Index);
        private const string LinkText = "Visit Air Traffic Control";

        public override string AreaName => Airport;

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Airport_default",
                url: "Airport/{controller}/{action}/{id}",
                defaults: new
                {
                    action = Index,
                    controller = AirTraffic,
                    id = UrlParameter.Optional
                }
            ).SetModuleRegistration(new MvcModule(AirTraffic, Index, Airport, LinkText), context);
        }
    }
}