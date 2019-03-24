using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Model
{
    /// <summary>
    /// Abstract class that provide propery wich is in concrete user and admin
    /// </summary>
    public abstract class AbstractUser
    {
        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
    }
}
