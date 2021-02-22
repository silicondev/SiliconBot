using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigmaBotCore
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract Task Code(DiscordMessage msg, string[] args);
    }
}
