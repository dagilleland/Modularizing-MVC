using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(MainWebApp.Startup))]

namespace MainWebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            CommonStartupCode.FileLog.Write(System.Web.HttpContext.Current.Server.MapPath("~"), $"{this.GetType().FullName}.{nameof(Configuration)}");

            // Run Startup of other modules. Order may matter if there are dependencies between them!
            Activator.CreateInstance<RefWeb.Startup>().Configuration(app);
        }
    }
}
