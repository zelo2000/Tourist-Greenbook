using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Model
{
    public class ConcreateUser : AbstractUser
    {
        List<Trip> Trips { get; set; }
    }
}
