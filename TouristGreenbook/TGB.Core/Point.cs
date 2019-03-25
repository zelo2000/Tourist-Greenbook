using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGB.WebAPI.Models
{
    public class Point
    {
        public int Id { get; set; }
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
