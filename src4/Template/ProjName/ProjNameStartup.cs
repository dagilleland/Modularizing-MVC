using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using $safeprojectname$.Areas.$safeprojectname$Area.Controllers;
using $safeprojectname$.Areas.$safeprojectname$Area.Models;

[assembly: OwinStartup(typeof($safeprojectname$.$safeprojectname$Startup))]

namespace $safeprojectname$
{
    public class $safeprojectname$Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            DefaultController.Presets.Add(new DemoModule { AssetName = "Index", Asset = AssetType.cshtml });
            DefaultController.Presets.Add(new DemoModule { AssetName = "About", Asset = AssetType.cshtml });
            DefaultController.Presets.Add(new DemoModule { AssetName = "SampleBase", Asset = AssetType.cshtml });
            DefaultController.Presets.Add(new DemoModule { AssetName = "_ViewStart", Asset = AssetType.cshtml });
            DefaultController.Presets.Add(new DemoModule { AssetName = "Demo", Asset = AssetType.js });
            DefaultController.Presets.Add(new DemoModule { AssetName = "Layout", Asset = AssetType.css });
            DefaultController.Presets.Add(new DemoModule { AssetName = "vs-file-properties-build-action", Asset = AssetType.png });
        }
        public void ApplyOptions(Options options)
        {
            DefaultController.HostAppName = options?.NameOfHostingApp;
        }
    }

    public class Options
    {
        public string NameOfHostingApp { get; set; }
    }

    public static class $safeprojectname$_OwinExtension
    {
        public static void Configure$safeprojectname$(this IAppBuilder app, Options options)
        {
            var owinStartup = new $safeprojectname$Startup();
            owinStartup.Configuration(app);
            owinStartup.ApplyOptions(options);

        }
    }
}