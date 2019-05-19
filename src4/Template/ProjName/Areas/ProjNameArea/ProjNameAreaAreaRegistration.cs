using System.Web.Mvc;

namespace $safeprojectname$.Areas.$safeprojectname$Area
{
    public class $safeprojectname$AreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "$safeprojectname$Area";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "$safeprojectname$Area_default",
                "$safeprojectname$Area/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}