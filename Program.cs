using DSharpPlus;
using DSharpPlus.Entities;
using SiliconBot.Events;
using SiliconBot.Events.TimedEvents;
using SiliconBot.Commands;
using SiliconBot.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace SiliconBot
{
    class Program
    {
        public static string BotName = "SiliconBot";
        public static string Version = "0.1-RELEASE";
        public static Random RNG = new Random();
        private static DateTime _startTime;

        public static List<ActiveUser> ActiveUsers = new List<ActiveUser>();
        public static List<ActiveUser> KnownUsers = new List<ActiveUser>();

        public static DiscordClient Client;

        private static List<BotEvent> _botEvents = new List<BotEvent>()
        {
            new CommandEvent()
        };
        private static List<TimeEvent> _timeEvents = new List<TimeEvent>()
        {
            new CheckActiveEvent(),
            new GiveComplimentEvent()
        };

        private static double _secondsAlive = 0;

        static async Task Main(string[] args)
        {
            try
            {
                Logger.Start();
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);

                Client = new DiscordClient(new DiscordConfiguration
                {
                    Token = Resources.BOT_TOKEN,
                    TokenType = TokenType.Bot
                });

                foreach (var evnt in _botEvents)
                {
                    Client.AddEvent(evnt);
                    Logger.Log($"{evnt.EventName} event added.", "bootup");
                }

                await Client.ConnectAsync();

                await Client.UpdateStatusAsync(new DiscordGame($"{BotName} v{Version}"), UserStatus.Online);

                Logger.Log("Bot is running.", "bootup");

                _startTime = DateTime.Now;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "bootup");
            }

            while (true)
            {
                foreach (var evnt in _timeEvents)
                {
                    await evnt.OnEvent(_secondsAlive);
                }

                _secondsAlive = _startTime.Difference(DateTime.Now).TotalSeconds;
            }
        }

        private static void OnExit(object sender, EventArgs e)
        {
            _ = Client.UpdateStatusAsync(user_status: UserStatus.Offline);
            Logger.Log("Received Shutdown command.", "shutdown");
            Logger.Stop();
        }
    }
}
