using System;
using System.Collections.Generic;
using System.Text;

namespace SiliconBot.Events
{
    public class TimeEventArgs : EventArgs
    {
        public double Seconds { get; set; }

        public TimeEventArgs(double secs)
        {
            Seconds = secs;
        }
    }
}
