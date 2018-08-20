using System;
using System.Linq;

namespace AirTrafficModule.Areas.Airport.Models
{
    public class RunwayClearance
    {
        public enum Purpose { Landing, Takeoff }

        public int FlightNumber { get; set; }
        public string Runway { get; set; }
        public Purpose Reason { get; set; }
    }
}