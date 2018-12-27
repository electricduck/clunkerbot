using System;
using System.Collections.Generic;
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

            // TODO: Add <h1> that bolds and uppercases text
            message = message
                .Replace("<footnote>", $"{Separator}" + Environment.NewLine)
                .Replace("</footnote>", String.Empty)
                .Replace("<li>", "• ")
                .Replace("</li>", String.Empty)
                .Replace("h2>", "b>")
                .Replace("h3>", "i>");

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

            string message = $@"{e.Message}
<code>{errorGuid}</code>
{Separator} 
<b>This is an error. Please forward me to </b>@theducky<b>.</b>";

            return BuildOutput(message, "He's dead, Jim!", "🚫");
        }

        public static string BuildSoftErrorOutput(string message)
        {
            return BuildOutput(message, "Oops!", "⚠");
        }

        public static string BuildSeeMoreString(Dictionary<string, string> links)
        {
            string output = "➡️ See more: ";

            foreach(var link in links) {
                output += $", <a href='{link.Key}'>{link.Value}</a>";
            }

            output += ".";
            output = output.Replace(": ,", ":");

            return output;
        }
    }
}