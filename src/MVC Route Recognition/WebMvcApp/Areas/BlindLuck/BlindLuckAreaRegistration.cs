using System.Web.Mvc;

namespace WebMvcApp.Areas.BlindLuck
{
    public class BlindLuckAreaRegistration : AreaRegistration
    {
        private const string BlindLuck = nameof(BlindLuck);
        private const string Yahtzee = nameof(Yahtzee);
        private const string Index = nameof(Index);
        private const string LinkText = "Play a Game";

        public override string AreaName => BlindLuck;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "BlindLuck_default",
                url: "BlindLuck/{controller}/{action}/{id}",
                defaults: new
                {
                    action = Index,
                    controller = Yahtzee,
                    id = UrlParameter.Optional
                }
            ).SetModuleRegistration(new MvcModule(Yahtzee, Index, BlindLuck, LinkText), context);
        }
    }
}