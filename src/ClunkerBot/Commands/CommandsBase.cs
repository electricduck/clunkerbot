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
            string headerFull = "";

            if(String.IsNullOrEmpty(input)) {
                headerFull = $"<i>{header}</i>";
            } else {
                headerFull = $"<i>{header}:</i> {input}";
            }

            message = message
                .Replace("header>", "b>")
                .Replace("subitem>", "i>");

            return $@"{emoji} {headerFull}
{Separator}
{message}";
        }

        public static string BuildOutput(
            string emoji,
            string header,
            string message
        )
        {
            return BuildOutput(emoji, header, null, message);
        }

        public static string BuildErrorOutput(Exception e)
        {
            Guid errorGuid = Guid.NewGuid();

            ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString(), errorGuid.ToString());

            string message = $@"{e.Message} Fuck.
<code>{errorGuid}</code>
{Separator} 
<b>This is an error. Please forward me to </b>@theducky<b>.</b>";

            return BuildOutput("⚠", "He's dead, Jim!", null, message);
        }
    }
}