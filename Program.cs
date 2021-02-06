using DSharpPlus;
using SiliconBot.Events;
using SiliconBot.Properties;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace SiliconBot
{
    class Program
    {
        static List<BotEvent> Events = new List<BotEvent>()
        {
            new CommandEvent()
        };

        static async Task Main(string[] args)
        {
            var client = new DiscordClient(new DiscordConfiguration
            {
                Token = Resources.BOT_TOKEN,
                TokenType = TokenType.Bot
            });

            foreach (var ev in Events)
            {
                ev.AddEvent(ref client);
                Console.WriteLine($"{ev.EventName} event added.");
            }

            await client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
