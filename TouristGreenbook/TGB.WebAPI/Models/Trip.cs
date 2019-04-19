using System;
using System.Collections.Generic;

namespace TGB.WebAPI.Models
{
    /// <summary>
    /// Class to work with the trip
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
        /// Start time spent in the trip
        /// </summary>
        public DateTime StayTimeStart { get; set; }
        /// <summary>
        /// Finish time spent in the trip
        /// </summary>
        public DateTime StayTimeFinish { get; set; }
        /// <summary>
        /// Money spent by the user on the trip
        /// </summary>
        public double Budget { get; set; }
        /// <summary>
        /// User who make trip
        /// </summary>
        public ConcreteUser ConcreteUser { get; set; }
        /// <summary>
        /// User id who make trip
        /// </summary>
        public string ConcreteUserId { get; set; }
    }
}
