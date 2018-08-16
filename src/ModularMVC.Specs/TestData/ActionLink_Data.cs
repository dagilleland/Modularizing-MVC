using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
// Credits: https://andrewlock.net/creating-strongly-typed-xunit-theory-test-data-with-theorydata/

namespace ModularMVC.Specs.TestData
{
    public class ActionLink_Data
    {
        /// <summary>WeaponsConroller</summary>
        private const string Weapons = nameof(Weapons);
        /// <summary>WeaponsConroller</summary>
        private const string Tactical = nameof(Tactical);

        public static TheoryData<string, string> ActionController => 
            new TheoryData<string, string>
            {
                { "FirePhasers", Weapons },
                { "LaunchTorpedoes", Weapons },
                { "RaiseShields", Tactical }
            };
    }
}
