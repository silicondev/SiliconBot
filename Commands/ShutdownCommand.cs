using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiliconBot.Commands
{
    public class ShutdownCommand : Command
    {
        public override string Name => "shutdown";

        public override async Task Code(DiscordMessage msg, string[] args)
        {
            await msg.RespondAsync("Goodbye!");
            Environment.Exit(0);
        }
    }
}
