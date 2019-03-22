using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Model
{
    public class Administrator : AbstractUser
    {
        string City { get; set; }
        List<Place> ConfirmedPlaces { get; set; }
    }
}
