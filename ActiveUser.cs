using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiliconBot
{
    public class ActiveUser
    {
        public DiscordUser User { get; }
        public DateTime LastSeen { get; set; }
        public DiscordChannel Channel { get; set; }

        public ActiveUser(DiscordUser user, DateTime? time = null, DiscordChannel channel = null)
        {
            User = user;
            if (time == null)
                LastSeen = DateTime.Now;
            else
                LastSeen = time.Value;
            Channel = channel;
        }
    }
}
