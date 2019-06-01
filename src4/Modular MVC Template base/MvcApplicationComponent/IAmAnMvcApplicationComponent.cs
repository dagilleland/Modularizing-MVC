using System;
using System.Linq;

namespace MvcApplicationComponent
{
    public interface IAmAnMvcApplicationComponent
    {
        AppRegistration MvcAppRegistration { get; }
    }
}
