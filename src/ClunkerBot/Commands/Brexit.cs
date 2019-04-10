using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using ClunkerBot.Data;
using ClunkerBot.Models;
using ClunkerBot.Models.ReturnModels;
using ClunkerBot.Models.ReturnModels.MessageReturnModels;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    class Brexit : CommandsBase
    {
        public static string TimeUntil()
        {
            DateTime originalDeadline = DateTime.Parse("Mar 29, 2019 23:00:00 +00:00");
            DateTime CurrentDeadline = DateTime.Parse("Apr 12, 2019 23:00:00 +01:00");
            DateTime MinimumExtensionDeadline = DateTime.Parse("May 22, 2019 23:00:00 +01:00");
            DateTime MaximumExtensionDeadline = DateTime.Parse("Jun 30, 2019 23:00:00 +01:00");

            string originalDeadlineOutput = OutputTimeUntil(originalDeadline, DateTime.UtcNow, "Delayed!");
            string CurrentDeadlineOutput = OutputTimeUntil(CurrentDeadline, DateTime.UtcNow.AddHours(1));
            string MinimumExtensionOutput = OutputTimeUntil(MinimumExtensionDeadline, DateTime.UtcNow.AddHours(1));
            string MaximumExtensionOutput = OutputTimeUntil(MaximumExtensionDeadline, DateTime.UtcNow.AddHours(1));
            
            string result = $@"<b>Original:</b> {originalDeadlineOutput}
<i>Original planned date of Brexit, now postponed (see below).</i>

<b>If Deal Rejected:</b> {CurrentDeadlineOutput}
<i>If the EU deny the request for an Article 50 extension, this is when the UK will leave the EU — this potentially causes the UK to exit the EU with no deal.</i>

<b>If A50 Extension (short):</b> {MinimumExtensionOutput}
<i>If the Article 50 extension is accepted, there is a chance Brexit will be delayed until the day before the EU Elections, which occur on the 23rd of May.</i>

<b>If A50 Extension (long):</b> {MaximumExtensionOutput}
<i>If the full Article 50 extension is accepted, Brexit will be delayed up until the 30th of June, after the European elections, but before the first session of new parliament.</i>

<i>It is currently unclear whether Theresa May will have negotiated an agreeable deal by any of these deadlines.</i>";
            return BuildOutput(result, "Time Until Brexit", "🇬🇧");
        }

        private static string OutputTimeUntil(DateTime deadline, DateTime now, string expired = "Time's up, May!")
        {
            TimeSpan timeUntil = deadline.Subtract(now);

            if(deadline < DateTime.Now)
            {
                return $"<i>{expired}</i>";
            }
            else
            {
                return timeUntil.ToString("'<b>'d'</b> Days, 'h' Hours, 'm' Minutes, 's' Seconds'");
            }
        }
    }
}
