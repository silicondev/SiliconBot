using SiliconBot.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SiliconBot.Program;

namespace SiliconBot.Events.TimedEvents
{
    public class GiveComplimentEvent : TimeEvent
    {
        public override float SecondsTillRun => 300;

        public override double LastRunAt { get; set; }

        public override string EventName => "GiveCompliment";

        private bool _reported = false;

        internal override async Task RunEvent()
        {
            if (!_reported)
                Logger.Log("Attempting to give compliment...", "compliments");
            if (ActiveUsers.Any())
            {
                var usr = ActiveUsers[RNG.Next(ActiveUsers.Count)];
                Logger.Log($"Active user found: {usr.User.Username}", "compliments");
                var comp = new ComplimentCommand();
                await comp.Compliment(usr.User);
                _reported = false;
            }
            else
            {
                if (!_reported)
                    Logger.Log("No active users.", "compliments", LogOrigin.SYS, LogType.WRN);
                _reported = true;
            }
        }
    }
}
