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
            DateTime originalDate = DateTime.Parse("Mar 29, 2019 00:00:00 +00:00");
            DateTime thirdExtensionDate = DateTime.Parse("Jan 31, 2020 00:00:00 +00:00");
            DateTime article50InvokedDate = DateTime.Parse("Mar 29, 2017 00:00:00 +00:00");
            DateTime referendumDate = DateTime.Parse("Jun 23, 2016 00:00:00 +00:00");

            string thirdExtensionOutput = OutputTimeUntil(thirdExtensionDate, DateTime.UtcNow);
            string originalOutput = OutputTimeSince(originalDate, DateTime.UtcNow);
            string article50InvokedOutput = OutputTimeSince(article50InvokedDate, DateTime.UtcNow);
            string referendumOutput = OutputTimeSince(referendumDate, DateTime.UtcNow);
            
            string result = $@"<h2>Third Extension (Current)</h2> 
➡️ {thirdExtensionOutput}
<i>As requested by Boris Johnson (under the Benn Act), making this the third extension to Brexit (as a deal has yet to be sorted), this is when the UK is due to leave the EU. Also of note this is a so-called 'flextension', allowing the UK to leave the EU before this time.</i>

<h2>Since Original Brexit</h2>
⬅️ {originalOutput}
<i>On 29-Mar-2019, Brexit is supposed to occur. An extension is requested by Theresa May.</i>

<h2>Since Article 50 Invoked</h2>
⬅️ {article50InvokedOutput}
<i>On 29-Mar-2017, Article 50 is triggered by Theresa May.</i>

<h2>Since Referendum</h2>
⬅️ {referendumOutput}
<i>On 23-Jun-2016, the UK holds the referendum: 51.9% of voters vote to leave. This creates Article 50: Brexit.</i>";
            
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
