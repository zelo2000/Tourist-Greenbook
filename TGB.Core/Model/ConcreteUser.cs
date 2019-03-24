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
        /// List of user trips
        /// </summary>
        List<Trip> Trips { get; set; }
    }
}
