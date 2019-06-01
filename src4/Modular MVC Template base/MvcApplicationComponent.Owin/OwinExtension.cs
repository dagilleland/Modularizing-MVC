using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplicationComponent.Owin
{
    public static class OwinExtension
    {
        public static void RegisterMvcComponents(this IAppBuilder self, params Assembly[] assemblies)
        {
            foreach (var assem in assemblies)
            {
                foreach (var app in assem.GetTypes().Where(x => typeof(IAmAnMvcApplicationComponent).IsAssignableFrom(x)))
                {
                    IAmAnMvcApplicationComponent x = assem.CreateInstance(app.FullName) as IAmAnMvcApplicationComponent;
                    self.Properties.Add($"MvcApp:{x.MvcAppRegistration.FriendlyName}", x.MvcAppRegistration);
                }
            }
        }
    }
}
