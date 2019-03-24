using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Utility
{
    /// <summary>
    /// Struct that provide 2D point
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
