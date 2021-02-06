using DSharpPlus;
using DSharpPlus.EventArgs;
using SiliconBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static SiliconBot.Program;

namespace SiliconBot.Events
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

            if (ActiveUsers.HasUser(author))
            {
                var usr = ActiveUsers.Find(x => x.User == author);
                usr.Channel = msg.Channel;
                usr.LastSeen = DateTime.Now;
            } else
            {
                ActiveUsers.Add(new ActiveUser(author, DateTime.Now, msg.Channel));
            }

            Logger.Log($"[MSG] {msg.Author.Username}: {text}");

            if (text.StartsWith(c))
            {
                string line = text.ToLower().Substring(1);
                var items = line.Split(" ");
                var name = items[0];
                var args = items.Skip(1).ToArray();

                foreach (var cmd in _commands)
                {
                    if (cmd.Name == name)
                    {
                        string str = $"Command received: {name}";
                        str += args.Any() ? $" with args {args.Combine()}" : "";
                        Logger.Log(str);

                        await cmd.Code(msg, args);
                        break;
                    }
                }
            }
        }
    }
}
