using DSharpPlus;
using DSharpPlus.Entities;
using SiliconBot.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SiliconBot.Program;

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

        public static T GetRandom<T>(this List<T> list) => list[RNG.Next(list.Count)];

        public static bool HasUser(this List<ActiveUser> list, DiscordUser user) => list.HasUser(user.Username);
        public static bool HasUser(this List<ActiveUser> list, string username)
        {
            foreach (var item in list)
            {
                if (item.User.Username == username)
                    return true;
            }
            return false;
        }

        public static bool RemoveUser(this List<ActiveUser> list, DiscordUser user) => list.RemoveUser(user.Username);
        public static bool RemoveUser(this List<ActiveUser> list, string username)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (item.User.Username == username)
                {
                    list.RemoveAt(i);
                    return true;
                } 
            }
            return false;
        }

        public static double Get(this Dictionary<string, double> dict, string key) => dict.ContainsKey(key) ? dict[key] : 0;

        public static TimeSpan Difference(this DateTime current, DateTime other) => other > current ? other - current : current - other;
    }
}
