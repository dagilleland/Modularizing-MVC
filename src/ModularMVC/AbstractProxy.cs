using System;
using System.Linq;

namespace ModularMVC
{
    public sealed class GlobalProxy : AbstractProxy
    {
        private static AbstractProxy _Instance;
        public static AbstractProxy Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new GlobalProxy();
                return _Instance;
            }
        }
    }
    public abstract class AbstractProxy : IProxy
    {
        private static string _Proxy;
        public string Proxy { get => _Proxy; set => _Proxy = value; }
    }
}
