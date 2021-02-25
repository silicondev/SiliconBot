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
        public static string Version = "0.2-RELEASE";
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

                await Client.InitializeAsync();

                await Client.UpdateStatusAsync(new DiscordGame($"{BotName} v{Version}"), UserStatus.Online); // This doesn't work

                var status = Client.CurrentUser.Presence.Game.Name;

                Logger.Log($"Bot is running with version {Version}.", "bootup");

                _startTime = DateTime.Now;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "bootup");
            }

            var consoleIn = Task.Run(() => Console.ReadLine());
            var commandRun = new CommandEvent();
            while (true)
            {
                foreach (var evnt in _timeEvents)
                {
                    await evnt.OnEvent(_secondsAlive);
                }

                if (consoleIn.IsCompleted)
                {
                    var result = consoleIn.Result;
                    if (!string.IsNullOrEmpty(result))
                    {
                        await commandRun.ParseCommand(result, new CommandContext(Context.CONS));
                        consoleIn = null;
                        consoleIn = Task.Run(() => Console.ReadLine());
                    }
                }

                _secondsAlive = _startTime.Difference(DateTime.Now).TotalSeconds;
            }
        }

        private static void OnExit(object sender, EventArgs e)
        {
            _ = Client.UpdateStatusAsync(user_status: UserStatus.Offline);
            _ = Client.DisconnectAsync();
            Logger.Log("Received Shutdown command.", "shutdown");
            Logger.Stop();
        }
    }
}
