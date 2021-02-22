using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiliconBot.Events
{
    public abstract class BotEvent
    {
        public abstract Task OnEvent(EventArgs e);
        public abstract void AddEvent(ref DiscordClient client);
        public abstract string EventName { get; }
    }
}
