using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaBotCore
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

        public static int Get(this Dictionary<string, int> dict, string key)
        {
            if (dict.ContainsKey(key))
                return dict[key];
            else
                return 0;
        }
    }
}
