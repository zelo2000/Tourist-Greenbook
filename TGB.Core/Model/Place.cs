using System;
using System.Collections.Generic;
using System.Text;
using TGB.Core.Enums;
using TGB.Core.Utility;

namespace TGB.Core.Model
{
    /// <summary>
    /// Сlass to work with the places
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Place name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Place type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Сity in which place is located
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Place Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Place working time
        /// </summary>
        public TimeInterval WorkTime { get; set; }
        /// <summary>
        /// Place coordinates
        /// </summary>
        public Point Coordinates { get; set; }
        /// <summary>
        /// Place state
        /// </summary>
        public PlaceState State { get; set; }

        public Place() { }
    }
}
