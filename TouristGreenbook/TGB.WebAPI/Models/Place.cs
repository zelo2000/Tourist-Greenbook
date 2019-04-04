using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.WebAPI.Models
{
    /// <summary>
    /// Сlass to work with the places
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Place id
        /// </summary>
        public int Id { get; set; }
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
        /// Place working start time
        /// </summary>
        public TimeSpan WorkTimeStart { get; set; }
        /// <summary>
        /// Place working finish time
        /// </summary>
        public TimeSpan WorkTimeFinish { get; set; }
        /// <summary>
        /// Place coordinates
        /// </summary>
        public Point Coordinates { get; set; }
        /// <summary>
        /// Place state
        /// </summary>
        public PlaceState State { get; set; }
        /// <summary>
        /// Place description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Place rating
        /// </summary>
        public double Rating { get; set; }
    }
}
