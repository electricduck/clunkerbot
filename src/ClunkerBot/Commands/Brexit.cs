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

            string currentDeadlineOutput = OutputTimeUntil(currentDeadline, DateTime.UtcNow);
            string extendedDeadlineOutput = OutputTimeUntil(extendedDeadline, DateTime.UtcNow);
            
            string result = $@"{currentDeadlineOutput}
<i>Under the current extension, this is when the UK is due to leave the EU. It is still unclear if a 'No Deal' will occur.</i>
            
{extendedDeadlineOutput}
<i>If another extension is accepted by the EU, as requested by Boris Johnson (under the Benn Act), this is when the UK is due to leave the EU.</i>";
            
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
                return timeUntil.ToString("'<b>'d'</b> Days, <b>'h'</b> Hours, <b>'m'</b> Minutes, <b>'s'</b> Seconds'");
            }
        }
    }
}
