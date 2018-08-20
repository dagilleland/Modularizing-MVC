using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcModularization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRouteHome
    {
        string DefaultController { get; }
        string DefaultAction { get; }
        string DefaultArea { get; }
        string DefaultLinkText { get; }
        ICollection<string> Namespaces { get; }
    }
}
