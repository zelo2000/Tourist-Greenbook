using System;
using System.Collections.Generic;

namespace TGB.WebAPI.Models
{
    /// <summary>
    /// Сlass to work with the trip
    /// </summary>
    public class TripWithPlaces 
    {
        /// <summary>
        /// List of places the user visited in this trip
        /// </summary>
        public List<Trip> Trips { get; set; }
        /// <summary>
        /// List of places the user visited in this trip
        /// </summary>
        public List<Place> Places { get; set; }
        /// <summary>
        /// List of all places the user can add to the trip
        /// </summary>
        public List<Place> AvailablePlaces { get; set; }
        /// <summary>
        /// List of places the user visited in the trip
        /// </summary>
        public SortedDictionary<Trip, List<Place>> PlacesInTrip { get; set; }
    }
}
