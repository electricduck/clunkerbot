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
            DateTime currentDeadline = DateTime.Parse("Oct 31, 2019 23:00:00 +00:00");
            DateTime extendedDeadline = DateTime.Parse("Jan 31, 2020 00:00:00 +00:00");
            DateTime sinceInvoked = DateTime.Parse("Mar 29, 2017 00:00:00 +00:00");

            string currentDeadlineOutput = OutputTimeUntil(currentDeadline, DateTime.UtcNow);
            string extendedDeadlineOutput = OutputTimeUntil(extendedDeadline, DateTime.UtcNow);
            string sinceInvokedOutput = OutputTimeSince(sinceInvoked, DateTime.UtcNow);
            
            string result = $@"<h2>Second Extension (Current)</h2>
➡️ {currentDeadlineOutput}
<i>Under the current extension, this is when the UK is due to leave the EU. It is still unclear if a 'No Deal' will occur.</i>

<h2>Third Extension (Proposed)</h2> 
➡️ {extendedDeadlineOutput}
<i>If another extension is accepted by the EU, as requested by Boris Johnson (under the Benn Act), this is when the UK is due to leave the EU.</i>

<h2>Since Article 50 Invoked</h2>
⬅️ {sinceInvokedOutput}
<i>On 29th March 2017, after a successful vote, Article 50 was triggered by Theresa May.</i>";
            
            return BuildOutput(result, "Time Until Brexit", "🇬🇧");
        }

        private static string OutputTimeUntil(DateTime deadline, DateTime now, bool reverse = false, string expired = "Time's up, Johnson!")
        {
            TimeSpan timeUntil = deadline.Subtract(now);

            if(!reverse)
            {
                if(deadline < DateTime.Now)
                {
                    return $"<i>{expired}</i>";
                }
            }

            return timeUntil.ToString("'<b>'d'</b> Days, <b>'h'</b> Hours, <b>'m'</b> Minutes, <b>'s'</b> Seconds'");
        }

        private static string OutputTimeSince(DateTime deadline, DateTime now)
        {
            return OutputTimeUntil(deadline, now, true);
        }
    }
}
