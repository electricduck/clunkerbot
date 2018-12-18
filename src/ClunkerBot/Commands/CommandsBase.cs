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

            return $@"{emoji} {headerFull}
{Separator}
{message}";
        }

        public static string BuildErrorOutput(Exception e)
        {
            ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());

            string message = $@"{e.Message}
<code>{Guid.NewGuid()}</code>
{Separator} 
<b>This is an error. Please forward me to </b>@theducky<b>.</b>";

            return BuildOutput("⚠", "He's dead, Jim!", null, message);
        }
    }
}