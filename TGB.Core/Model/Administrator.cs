using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Model
{
    /// <summary>
    /// Сlass to work with the administrators  
    /// </summary>
    public class Administrator : AbstractUser
    {
        /// <summary>
        /// City in which administrator can confirm places
        /// </summary>
        string City { get; set; }
        /// <summary>
        /// List of places approved by the administrator
        /// </summary>
        List<Place> ApprovedPlaces { get; set; }
    }
}
