using System;
using System.Linq;

namespace ModularMVC
{
    public abstract class AbstractProxy : IProxy
    {
        private static string _Proxy;
        public string Proxy { get => _Proxy; set => _Proxy = value; }
    }
}
