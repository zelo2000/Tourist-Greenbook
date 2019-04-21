using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGB.WebAPI.Models
{
    /// <summary>
    /// Сlass to work with concrete user 
    /// </summary>
    public class ConcreteUser : IdentityUser
    {
        /// <summary>
        /// List of user trips
        /// </summary>
        List<Trip> Trips { get; set; }

        public ConcreteUser()
        {
            Trips = new List<Trip>();
        }
    }
}
