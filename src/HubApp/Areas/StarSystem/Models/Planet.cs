using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HubApp.Areas.StarSystem.Models
{
    public class PlanetaryBody
    {
        public int Orbit { get; set; }
        public string Name { get; set; }
    }
    public static class SolSystem
    {
        public static IDictionary<string, PlanetaryBody> Planets
            = new Dictionary<string, PlanetaryBody>
            {
                {"Mercury", new PlanetaryBody { Name = "Mercury", Orbit = 1 } },
                {"Venus", new PlanetaryBody { Name = "Venus", Orbit = 2 } },
                {"Earth", new PlanetaryBody { Name = "Earth", Orbit = 3 } },
                {"Mars", new PlanetaryBody { Name = "Mars", Orbit = 4 } }
            };
    }
}