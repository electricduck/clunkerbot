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
            DateTime thirdDeadline = DateTime.Parse("Jan 31, 2020 00:00:00 +00:00");
            DateTime sinceInvoked = DateTime.Parse("Mar 29, 2017 00:00:00 +00:00");

            string thirdDeadlineOutput = OutputTimeUntil(thirdDeadline, DateTime.UtcNow);
            string sinceInvokedOutput = OutputTimeSince(sinceInvoked, DateTime.UtcNow);
            
            string result = $@"<h2>Third Extension (Current)</h2> 
➡️ {thirdDeadlineOutput}
<i>As requested by Boris Johnson (under the Benn Act), making this the third extension to Brexit (as a dead has yet to be sorted) this is when the UK is due to leave the EU. Also of note this is a so-called 'flextension', allowing the UK to leave the EU before this time.</i>

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
