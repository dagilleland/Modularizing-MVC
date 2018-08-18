using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HubApp.Areas.StarSystem.Models
{
    public class PlanetaryBody
    {
        [Description("The position of the orbit with respect to the star")]
        public int Orbit { get; set; }
        [Description("The common name of the planetary body")]
        [Required(ErrorMessage = "Planet name is required")]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }
    }
}