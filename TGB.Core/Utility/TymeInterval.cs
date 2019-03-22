using System;
using System.Collections.Generic;
using System.Text;

namespace TGB.Core.Utility
{
    public class TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }

        public TimeInterval() { }

        public TimeInterval(DateTime start, DateTime finish)
        {
            Start = start;
            Finish = finish;
        }
    }
}
