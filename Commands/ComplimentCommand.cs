﻿using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SiliconBot.Program;

namespace SiliconBot.Commands
{
    public class ComplimentCommand : Command
    {
        private List<string> _compliments = new List<string>();

        public override string Name => "getcompliment";

        public override async Task Code(CommandContext msg, string[] args)
        {
            await Compliment(msg.Msg.Author);
        }

        public async Task Compliment(DiscordUser user)
        {
            if (!_compliments.Any())
            {
                string[] compFile = { };
                try
                {
                    compFile = File.ReadAllLines(@"compliments.txt");
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "compliments", LogOrigin.CMD);
                    return;
                }
                
                _compliments = new List<string>(compFile);

                foreach (var line in _compliments)
                {
                    if (string.IsNullOrEmpty(line)) _compliments.Remove(line);
                }
            }

            var usr = KnownUsers.Find(x => x.User == user);
            var channel = usr.Channel;
            await channel.SendMessageAsync($"{usr.User.Mention} - {_compliments[RNG.Next(_compliments.Count)]}");
        }
    }
}
