using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcApplicationComponent
{
    public class AppRegistration
    {
        public string FriendlyName { get; set; }
        public UIAssembly UserInterface { get; set; }
        public IEnumerable<DacPacInfo> DacPacDependencies { get; set; }
            = new HashSet<DacPacInfo>();
        public MvcActionLink AppHomePage { get; set; }
    }
}
