using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClunkerBot.Utilities
{
    class StringUtilities
    {
        public static string AddSpacesToSentence(string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) && 
                        i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public static int CountWords(string s)
        {
            MatchCollection collection = Regex.Matches(s, @"[\S]+");
            return collection.Count;
        }

        public static string EmojiForCountry(string code)
        {
            switch(code)
            {
                case "gb":
                case "uk":
                    return "🇬🇧 gb/uk";
            }

            return code;
        }

        public static string EmojiForCountryList(string[] codeList)
        {
            string result = "";

            for (int i = 0; i < codeList.Count(); i++) {
                if (i == codeList.Count() - 1)
                {
                    result = $"";
                }
                else
                {

                }
            }

            foreach(string code in codeList)
            {
                result += EmojiForCountry(code);
            }

            return result;
        }
    }
}