using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace SiliconBot
{
    public class CommandContext
    {
        public Context Context { get; }
        public DiscordMessage Msg { get; }

        public async Task RespondAsync(string message)
        {
            switch (Context)
            {
                case Context.DMSG:
                    await Msg.RespondAsync(message);
                    break;
                case Context.CONS:
                    Logger.Log(message, "input");
                    break;
            }
        }

        public CommandContext(Context context, DiscordMessage dMsg = null)
        {
            Context = context;
            Msg = dMsg;
        }
    }

    public enum Context
    {
        DMSG,
        CONS
    }
}
