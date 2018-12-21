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
            string message,
            string header,
            string emoji,
            string input
        )
        {
            string output = "";

            string headerFull = "";

            if(String.IsNullOrEmpty(input)) {
                headerFull = $"<i>{header}</i>";
            } else {
                headerFull = $"<i>{header}:</i> {input}";
            }

            if(String.IsNullOrEmpty(emoji)) {
                headerFull = $"{headerFull}";
            } else {
                headerFull = $"{emoji} {headerFull}";
            }

            message = message
                .Replace("header>", "b>")
                .Replace("subitem>", "i>")
                .Replace("item>", "b>")
                .Replace("<subitem-bullet>", "• ")
                .Replace("</subitem-bullet>", String.Empty)
                .Replace("<subitem-icon>", String.Empty)
                .Replace("</subitem-icon>", String.Empty)
                .Replace("<subtext>", String.Empty)
                .Replace("</subtext>", String.Empty)
                .Replace("<footnote>", $"{Separator}" + Environment.NewLine)
                .Replace("</footnote>", String.Empty);

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
            string message,
            string header,
            string emoji
        )
        {
            return BuildOutput(message, header, emoji, null);
        }

        public static string BuildOutput(
            string message
        )
        {
            return BuildOutput(message, null, null, null);
        }

        public static string BuildErrorOutput(Exception e)
        {
            if(e.Message.Contains("not recognized as a valid DateTime"))
            {
                return BuildSoftErrorOutput("Invalid date. Try using the format '01-Jan-2000'.");
            }

            Guid errorGuid = Guid.NewGuid();

            ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString(), errorGuid.ToString());

            string message = $@"{e.Message} Fuck.
<code>{errorGuid}</code>
{Separator} 
<b>This is an error. Please forward me to </b>@theducky<b>.</b>";

            return BuildOutput(message, "He's dead, Jim!", "🚫");
        }

        public static string BuildSoftErrorOutput(string message)
        {
            return BuildOutput(message, "Oops!", "⚠");
        }
    }
}