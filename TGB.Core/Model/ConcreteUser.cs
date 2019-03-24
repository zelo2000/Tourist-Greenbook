using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Model
{
    /// <summary>
    /// Сlass to work with concrete user 
    /// </summary>
    public class ConcreteUser : AbstractUser
    {
        /// <summary>
        /// User id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of user trips
        /// </summary>
        List<Trip> Trips { get; set; }
    }
}
