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
            DateTime implementationDate = DateTime.Parse("Jan 1, 2021 00:00:00 +00:00");
            DateTime brexitDate = DateTime.Parse("Jan 31, 2020 23:00:00 +00:00");

            string implementationOutput = OutputTimeUntil(implementationDate, DateTime.UtcNow);
            string brexitOutput = OutputTimeSince(brexitDate, DateTime.UtcNow);
            
            string result = $@"<h2>Brexit Implementation</h2>
⬅️ {implementationOutput}
<i>During the 11-month period since 'Brexit Day', the transition period will start, allowing negotiations to the future UK-EU relationship. During this time, the UK will continue to follow all of EU's rules and trading agreements.</i>
            
<h2>Since Brexit</h2> 
⬅️ {brexitOutput}
<i>On 31-Jan-2020, Brexit occurs: the UK (finally) leaves the European Union.</i>";
            
            return BuildOutput(result, "Time Until Brexit", "🇬🇧");
        }

        private static string OutputTimeUntil(DateTime deadline, DateTime now, bool reverse = false, string expired = "Time's up, Johnson!")
        {
            TimeSpan timeUntil = deadline.Subtract(now);

            if(!reverse)
            {
                if(deadline < DateTime.Now)
                {
                    return $"{expired}";
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
