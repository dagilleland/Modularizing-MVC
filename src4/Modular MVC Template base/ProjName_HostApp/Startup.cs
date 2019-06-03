using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using ProjName;
using MvcApplicationComponent;
using MvcApplicationComponent.Owin;
using System.Runtime.InteropServices;

[assembly: OwinStartup(typeof(HostApp.Startup))]

namespace HostApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            // Register the Host Component
            app.RegisterMvcComponents(this.GetType().Assembly);
            // Register additional components
            // TODO: List all loaded assemblies, except the host assembly
            app.RegisterMvcComponents();
            app.ConfigureProjName(new Options
            {
                NameOfHostingApp = "My ASP.NET Application"
            });
        }

        public class HostComponent : AppRegistration, IAmAnMvcApplicationComponent
        {
            public HostComponent()
            {
                FriendlyName = "Host MVC Application";
                AppHomePage = new MvcActionLink
                {
                    Controller = "Home",
                    LinkText = "Host Application"
                };
                var assem = this.GetType().Assembly;
                var guid = assem.GetCustomAttributes(typeof(GuidAttribute), false)[0] as GuidAttribute;
                UserInterface = new UIAssembly
                {
                    AssemblyGuid = Guid.Parse(guid.Value),
                    FullyQualifiedAssemblyName = assem.FullName
                };
            }
            public AppRegistration MvcAppRegistration => this;
        }
    }
}
