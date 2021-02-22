using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SiliconBot.Extensions;
using static SiliconBot.Program;

namespace SiliconBot.Events.TimedEvents
{
    public class CheckActiveEvent : TimeEvent
    {
        public override float SecondsTillRun => 5;

        public override double LastRunAt { get; set; }

        public override string EventName => "CheckActive";

        private static List<ActiveUser> _userCache = new List<ActiveUser>();

        internal override async Task RunEvent()
        {
            List<ActiveUser> usrs = new List<ActiveUser>();
            List<ActiveUser> added = new List<ActiveUser>();
            List<ActiveUser> removed = new List<ActiveUser>();

            foreach (var usr in KnownUsers)
            {
                if (usr.LastSeen.AddMinutes(5) > DateTime.Now)
                {
                    usrs.Add(usr);
                    if (!_userCache.Contains(usr))
                    {
                        added.Add(usr);
                    }
                }
                else
                {
                    if (_userCache.Contains(usr))
                        removed.Add(usr);
                }
            }

            ActiveUsers = new List<ActiveUser>(usrs);
            _userCache = new List<ActiveUser>(usrs);

            foreach (var usr in added)
            {
                Logger.Log($"{usr.User.Username} +", "active", LogOrigin.USR);
            }
            foreach (var usr in removed)
            {
                Logger.Log($"{usr.User.Username} -", "active", LogOrigin.USR);
            }
        }
    }
}
