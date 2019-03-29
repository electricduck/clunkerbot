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
            DateTime backDealDeadline = DateTime.Parse("May 22, 2019 23:00:00 +01:00");
            DateTime noDealDeadline = DateTime.Parse("Apr 12, 2019 23:00:00 +01:00");

            string originalDeadlineOutput = OutputTimeUntil(originalDeadline, DateTime.UtcNow, "Delayed!");
            string backDealDeadlineOutput = OutputTimeUntil(backDealDeadline, DateTime.UtcNow.AddHours(1));
            string noDealDeadlineOutput = OutputTimeUntil(noDealDeadline, DateTime.UtcNow.AddHours(1));

            string result = $@"<b>Original:</b> {originalDeadlineOutput}
<i>Original planned date of Brexit, now postponed (see below).</i>

<b>If Deal Backed:</b> {backDealDeadlineOutput}
<i>If MPs approve the withdrawal deal on the 29th March, this is when the UK will leave the EU.</i>

<b>If Deal Rejected:</b> {noDealDeadlineOutput}
<i>If MPs reject the withdrawal deal on the 29th March, this is when the UK will leave the EU — this potentially causes the UK to exit the EU with no deal.</i>";

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