using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace C_Bootcamp_Fianl_Project
{
    internal class InputHandler
    {
        public int ReadInt()
        {
            return int.Parse(Console.ReadLine().Trim());
        }

        public Dictionary<string, string> ParseSearchInput(string input) {
            string pattern = @"-(\w+)\s+""?([^""-]*)""?";
            var matches = Regex.Matches(input, pattern);
            var flags = new Dictionary<string, string>();

            foreach (Match match in matches)
            {
                string flag = match.Groups[1].Value;
                string value = match.Groups[2].Value;
                if (flag == "t") // type
                    value = value.ToUpper();

                flags[flag] = value;

            }

            if (!flags.ContainsKey("t") || !flags.ContainsKey("q") || !flags.ContainsKey("p"))
                throw new Exception("Required flags not given");

            return flags;

        }

        public string[] SplitTypes(string input)
        {
            return input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
