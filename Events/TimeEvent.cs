using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiliconBot.Events
{
    public abstract class TimeEvent
    {
        public async Task OnEvent(double seconds)
        {
            if (LastRunAt != 0 && (seconds - LastRunAt) < SecondsTillRun)
                return;
            LastRunAt = seconds;
            await RunEvent();
        }

        internal abstract Task RunEvent();
        public abstract float SecondsTillRun { get; }
        public abstract double LastRunAt { get; set; }
        public abstract string EventName { get; }
    }
}
