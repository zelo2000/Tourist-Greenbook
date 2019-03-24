using System.Collections.Generic;
using TGB.Core.Utility;

namespace TGB.Core.Model
{
    /// <summary>
    /// Сlass to work with the trip
    /// </summary>
    public class Trip
    {
        /// <summary>
        /// Trip id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of places the user visited in this trip
        /// </summary>
        public List<Place> Places { get; set; }
        /// <summary>
        /// The city where the trip was
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Time spent in the trip
        /// </summary>
        public TimeInterval StayTime { get; set; }
        /// <summary>
        /// Money spent by the user on the trip
        /// </summary>
        public double Budget { get; set; }
    }
}
