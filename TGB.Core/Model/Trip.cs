using System.Collections.Generic;
using TGB.Core.Utility;

namespace TGB.Core.Model
{
    public class Trip
    {
        public List<Place> Places { get; set; }
        public string City { get; set; }
        public TimeInterval StayTime { get; set; }
        public double Budget { get; set; }
    }
}
