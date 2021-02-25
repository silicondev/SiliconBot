using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiliconBot.Commands
{
    public class MeowCommand : Command
    {
        public override string Name => "meow";

        public override async Task Code(CommandContext msg, string[] args)
        {
            await msg.RespondAsync("Meow.");
        }
    }
}
