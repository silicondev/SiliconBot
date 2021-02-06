using DSharpPlus;
using DSharpPlus.Entities;
using SiliconBot.Commands;
using SiliconBot.Events;
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
        public static Random RNG = new Random();
        public static List<ActiveUser> ActiveUsers = new List<ActiveUser>();
        public static List<ActiveUser> KnownUsers = new List<ActiveUser>();
        private static List<ActiveUser> _userCache = new List<ActiveUser>();
        public static DiscordClient Client;
        private static List<BotEvent> _events = new List<BotEvent>()
        {
            new CommandEvent()
        };
        private static int _secondsAlive = 0;
        private static Dictionary<string, int> LastRun = new Dictionary<string, int>();

        static async Task Main(string[] args)
        {
            try
            {
                Client = new DiscordClient(new DiscordConfiguration
                {
                    Token = Resources.BOT_TOKEN,
                    TokenType = TokenType.Bot
                });

                foreach (var evnt in _events)
                {
                    Client.AddEvent(evnt);
                    Logger.Log($"{evnt.EventName} event added.");
                }

                
                await Client.ConnectAsync();

                Logger.Log("Bot is running.");
                //await Task.Delay(-1);

                
                while (true)
                {
                    if (_secondsAlive >= LastRun.Get("CheckActive") + 5)
                    {
                        List<ActiveUser> usrs = new List<ActiveUser>();
                        List<ActiveUser> added = new List<ActiveUser>();
                        List<ActiveUser> removed = new List<ActiveUser>();

                        foreach (var usr in KnownUsers)
                        {
                            if (usr.LastSeen.AddMinutes(5) > DateTime.Now)
                            {
                                usrs.Add(usr);
                                if (!_userCache.Contains(usr))
                                {
                                    added.Add(usr);
                                }
                            } else
                            {
                                if (_userCache.Contains(usr))
                                    removed.Add(usr);
                            }
                        }

                        ActiveUsers = new List<ActiveUser>(usrs);
                        _userCache = new List<ActiveUser>(usrs);

                        foreach (var usr in added)
                        {
                            Logger.Log($"[USER] + {usr.User.Username}");
                        }
                        foreach (var usr in removed)
                        {
                            Logger.Log($"[USER] - {usr.User.Username}");
                        }

                        LastRun["CheckActive"] = _secondsAlive;
                    }

                    if (_secondsAlive >= LastRun.Get("GiveCompliment") + 300)
                    {
                        Logger.Log("Attempting to give compliment...");
                        if (ActiveUsers.Any())
                        {
                            var usr = ActiveUsers[RNG.Next(ActiveUsers.Count)];
                            Logger.Log($"Active user found: {usr.User.Username}");
                            var comp = new ComplimentCommand();
                            await comp.Compliment(usr.User);
                        } else 
                        {
                            Logger.Log("No active users.");
                        }
                        LastRun["GiveCompliment"] = _secondsAlive;
                    }


                    await Task.Delay(1000);
                    _secondsAlive++;
                }
            } 
            catch (Exception e)
            {
                Logger.Log($"Error when starting bot: {e.Message}", LogType.ERROR);
                Logger.Log(e.StackTrace, LogType.ERROR);
            }
        }
    }
}
