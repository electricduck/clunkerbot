using System.Text.RegularExpressions;

namespace CarPupsTelegramBot.Utilities
{
    class StringUtilities
    {
        public static int CountWords(string s)
        {
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }
    }
}