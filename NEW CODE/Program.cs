using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using SigmaBotCore.Events; // NEEDS CHANGING
using SigmaBotCore.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SigmaBotCore
{
    public class Program
    {
        static string AppName = "";
        public static int SecondsAlive = 0;
        private static DiscordClient _client;

        private static List<BotEvent> _events = new List<BotEvent>()
        {
            new CommandEvent()
        };

        private static Dictionary<string, int> _timeSince = new Dictionary<string, int>();


        public static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);

           _client = new DiscordClient(new DiscordConfiguration
            {
                Token = Resources.DiscordToken,
                TokenType = TokenType.Bot
            });

            foreach (var evnt in _events)
            {
                evnt.AddEvent(ref _client);
                Logger.Log($"{evnt.EventName} event added.", "bootup");
            }

            await _client.ConnectAsync();
            Logger.Log("Bot is running.", "bootup");

            DiscordGuild server;
            while (true)
            {
                //server = await _client.GetGuildAsync(806568746374004806); NEEDS REVISION

                // BOT LOOP
                
                await Task.Delay(1000);
                SecondsAlive++;
            }
        }

        public static async Task ChangeNickname(DiscordMember member, string nickname, bool append = false)
        {
            if (append) nickname = member.Username + nickname;
            await member.ModifyAsync(nickname: nickname);
        }

        private static void OnExit(object sender, EventArgs e)
        {
            Logger.Log("Received Shutdown command.", "shutdown");
            Logger.Stop();
        }
    }
}
