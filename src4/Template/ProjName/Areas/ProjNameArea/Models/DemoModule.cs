using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace $safeprojectname$.Areas.$safeprojectname$Area.Models
{
    public enum AssetType { UNKOWN, cshtml, js, css, png, jpg }

    public class DemoModule
    {
        public AssetType Asset { get; set; }
        public string AssetName { get; set; }
    }
}