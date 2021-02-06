using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SiliconBot.Program;

namespace SiliconBot.Commands
{
    public class ActiveUsersCommand : Command
    {
        public override string Name => "getactive";

        public override async Task Code(DiscordMessage msg, string[] args)
        {
            await msg.RespondAsync("Active Users:");
            foreach (var usr in ActiveUsers)
            {
                await msg.RespondAsync(usr.User.Username);
            }
            await msg.RespondAsync("===");
        }
    }
}
