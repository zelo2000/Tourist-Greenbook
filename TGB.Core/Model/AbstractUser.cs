using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Model
{
    public abstract class AbstractUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
