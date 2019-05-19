using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using ProjName.Areas.ProjNameArea.Controllers;
using ProjName.Areas.ProjNameArea.Models;

[assembly: OwinStartup(typeof(ProjName.ProjNameStartup))]

namespace ProjName
{
    public class ProjNameStartup
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

    public static class ProjName_OwinExtension
    {
        public static void ConfigureProjName(this IAppBuilder app, Options options)
        {
            var owinStartup = new ProjNameStartup();
            owinStartup.Configuration(app);
            owinStartup.ApplyOptions(options);

        }
    }
}