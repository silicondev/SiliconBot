using DSharpPlus;
using DSharpPlus.EventArgs;
using SigmaBotCore.Commands; // NEEDS CHANGING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaBotCore.Events
{
    public class CommandEvent : BotEvent
    {
        private const char c = '>';
        public override string EventName => "MessageCreated";

        private List<Command> _commands = new List<Command>()
        {
            // COMMANDS GO HERE
        };

        public override void AddEvent(ref DiscordClient client) => client.MessageCreated += OnEvent;

        public override async Task OnEvent(EventArgs e)
        {
            var ev = (MessageCreateEventArgs)e;

            var msg = ev.Message;
            var text = msg.Content;
            var author = msg.Author;
            var channel = msg.Channel;

            if (author.IsBot)
                return;

            Logger.Log($"{author.Username}: {text}", channel.Name, LogOrigin.MSG);

            if (text.StartsWith(c))
            {
                string line = text.ToLower().Substring(1);
                var items = line.Split(" ");
                var name = items[0];
                var args = items.Skip(1).ToArray();

                var cmd = _commands.First(x => x.Name == name);
                if (cmd != null)
                {
                    string str = $"Command received: {name}";
                    str += args.Any() ? $" with args {args.Combine()}" : "";
                    Logger.Log(str, channel.Name, LogOrigin.CMD);

                    await cmd.Code(msg, args);
                }
            } else {
				// NOT A COMMAND
			}
        }
    }
}
