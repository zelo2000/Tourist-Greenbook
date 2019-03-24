using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Utility
{
    /// <summary>
    /// Class that provide time interval
    /// </summary>
    public class TimeInterval
    {
        /// <summary>
        /// Start time
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// Finish time
        /// </summary>
        public DateTime Finish { get; set; }

        public TimeInterval() { }

        public TimeInterval(DateTime start, DateTime finish)
        {
            Start = start;
            Finish = finish;
        }
    }
}
