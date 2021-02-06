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
            try
            {
                var client = new DiscordClient(new DiscordConfiguration
                {
                    Token = Resources.BOT_TOKEN,
                    TokenType = TokenType.Bot
                });

                foreach (var evnt in Events)
                {
                    client.AddEvent(evnt);
                    Logger.Log($"{evnt.EventName} event added.");
                }

                Logger.Log("Bot is running.");
                await client.ConnectAsync();
                await Task.Delay(-1);
            } 
            catch (Exception e)
            {
                Logger.Log($"Error when starting bot: {e.Message}", LogType.ERROR);
                Logger.Log(e.StackTrace, LogType.ERROR);
            }
        }
    }
}
