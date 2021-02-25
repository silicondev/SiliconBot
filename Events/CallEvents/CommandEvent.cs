using DSharpPlus;
using DSharpPlus.EventArgs;
using SiliconBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static SiliconBot.Program;
using DSharpPlus.Entities;

namespace SiliconBot.Events.TimedEvents
{
    public class CommandEvent : BotEvent
    {
        private const char c = '>';
        public override string EventName => "MessageCreated";

        private List<Command> _commands = new List<Command>()
        {
            new PingCommand(),
            new ShutdownCommand(),
            new MeowCommand(),
            new ActiveUsersCommand(),
            new ComplimentCommand()
        };

        public override void AddEvent(ref DiscordClient client) => client.MessageCreated += OnEvent;

        public override async Task OnEvent(EventArgs e)
        {
            var ev = (MessageCreateEventArgs)e;

            var msg = ev.Message;
            var text = msg.Content;
            var author = msg.Author;
            var channel = msg.Channel;

            Logger.Log($"{author.Username}: {text}", channel.Name, LogOrigin.MSG);

            if (author.IsBot)
                return;

            if (KnownUsers.HasUser(author))
            {
                var usr = KnownUsers.Find(x => x.User == author);
                usr.Channel = msg.Channel;
                usr.LastSeen = DateTime.Now;
            } else
                KnownUsers.Add(new ActiveUser(author, DateTime.Now, channel));

            if (text.StartsWith(c))
            {
                string line = text.ToLower().Substring(1);
                await ParseCommand(line, new CommandContext(Context.DMSG, msg));
            }
        }

        public async Task ParseCommand(string text, CommandContext msg)
        {
            var items = text.Split(" ");
            var name = items[0];
            var args = items.Skip(1).ToArray();

            foreach (var cmd in _commands)
            {
                if (cmd.Name == name)
                {
                    string str = $"Command received: {name}";
                    str += args.Any() ? $" with args {args.Combine()}" : "";
                    Logger.Log(str, "command", LogOrigin.CMD);

                    await cmd.Code(msg, args);
                    break;
                }
            }
        }
    }
}
