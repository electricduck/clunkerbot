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

            string currentDeadlineOutput = OutputTimeUntil(currentDeadline, DateTime.UtcNow);
            
            string result = $@"{currentDeadlineOutput}";
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
