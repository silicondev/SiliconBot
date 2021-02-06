using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiliconBot
{
    public static class Extensions
    {
        public static string Combine(this string[] arr)
        {
            string output = "";
            int first = 1;
            foreach (var itm in arr)
            {
                output += first == 1 ? itm : $", {itm}";
                first = 0;
            }
            return output;
        }

        public static void AddEvent(this DiscordClient client, BotEvent evnt) => evnt.AddEvent(ref client);
    }
}
