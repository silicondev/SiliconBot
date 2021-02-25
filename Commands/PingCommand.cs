using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiliconBot.Commands
{
    public class PingCommand : Command
    {
        public override string Name => "ping";

        public override async Task Code(CommandContext msg, string[] args) 
        { 
            await msg.RespondAsync("Pong!");
            if (args.Length > 0)
            {
                await msg.RespondAsync("Arguments:");
                foreach (var arg in args)
                {
                    await msg.RespondAsync(arg);
                }
            }
        }
    }
}
