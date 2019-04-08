using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGB.WebAPI.Models
{
    public class YourTrips
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
    }
}
