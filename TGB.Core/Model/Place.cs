using System;
using System.Collections.Generic;
using System.Text;
using TGB.Core.Enums;
using TGB.Core.Utility;

namespace TGB.Core.Model
{
    public class Place
    {
        string Name { get; set; }
        string Type { get; set; }
        string City { get; set; }
        string Address { get; set; }
        TimeInterval WorkTime { get; set; }
        Point Coordinates { get; set; }
        PlaceState State { get; set; }

        public Place() { }
    }
}
