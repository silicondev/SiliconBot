using DSharpPlus;
using DSharpPlus.EventArgs;
using SiliconBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
            new MeowCommand()
        };

        public override void AddEvent(ref DiscordClient client)
        {
            client.MessageCreated += OnEvent;
        }

        public override async Task OnEvent(EventArgs e)
        {
            var ev = (MessageCreateEventArgs)e;

            var msg = ev.Message;
            var text = msg.Content;

            Console.WriteLine($"Message event received: {text}");

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
                        await cmd.Code(msg, args);
                        break;
                    }
                }
            }
        }
    }
}
