using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HubApp.Models
{
    public class Exploration
    {
        public int Id { get; set; }
    }
    public class AreaProxy
    {
        public string Proxy { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string PayloadData { get; set; }

        public override string ToString()
        {
            string text = $@"{Proxy}/{AreaName}/{ControllerName}";
            if (string.IsNullOrWhiteSpace(ActionName)) text += $@"/Index";
            else text += $@"/{ActionName}";
            return text;
        }
    }
}