﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using ProjName;

[assembly: OwinStartup(typeof(HostApp.Startup))]

namespace HostApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.ConfigureProjName(new Options
            {
                NameOfHostingApp = "My ASP.NET Application"
            });
        }
    }
}
