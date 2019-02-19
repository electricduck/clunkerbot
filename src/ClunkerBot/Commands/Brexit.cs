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
            DateTime deadline = DateTime.Parse("Mar 29, 2019 11:00:00");
            TimeSpan timeUntil = deadline.Subtract(DateTime.UtcNow);

            string result = "";

            if(deadline < DateTime.Now)
            {
                result = "<b>Fuck.</b>";
            }
            else
            {
                result = timeUntil.ToString("'<b>'d'</b> Days <b>'h'</b> Hours <b>'m'</b> Minutes <b>'s'</b> Seconds'");
            }

            return BuildOutput(result, "Time Until Brexit", "🇬🇧");
        }
    }
}