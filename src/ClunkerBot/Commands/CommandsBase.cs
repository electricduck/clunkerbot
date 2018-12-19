using System;
using System.Globalization;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    public class CommandsBase
    {
        public static TextInfo TextInfo_enUS = new CultureInfo("en-US",false).TextInfo;

        public static string Separator = "—";

        public static string BuildOutput(
            string emoji,
            string header,
            string input,
            string message
        )
        {
            string output = "";

            string headerFull = "";

            if(String.IsNullOrEmpty(emoji)) {
                headerFull = $"{headerFull}";
            } else {
                headerFull = $"{emoji} {headerFull}";
            }

            message = message
                .Replace("header>", "b>")
                .Replace("subitem>", "i>")
                .Replace("<subitem-icon>", String.Empty)
                .Replace("</subitem-icon>", String.Empty);

            if(String.IsNullOrEmpty(header)) {
                output = $@"{message}";
            } else {
                output = $@"{headerFull}
{Separator}
{message}";
            }

            return output;
        }

        public static string BuildOutput(
            string emoji,
            string header,
            string message
        )
        {
            return BuildOutput(emoji, header, null, message);
        }

        public static string BuildOutput(
            string message
        )
        {
            return BuildOutput(null, null, null, message);
        }

        public static string BuildErrorOutput(Exception e)
        {
            Guid errorGuid = Guid.NewGuid();

            ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString(), errorGuid.ToString());

            string message = $@"{e.Message} Fuck.
<code>{errorGuid}</code>
{Separator} 
<b>This is an error. Please forward me to </b>@theducky<b>.</b>";

            return BuildOutput("⚠", "He's dead, Jim!", message);
        }
    }
}