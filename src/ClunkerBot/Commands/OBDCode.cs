using System;
using HtmlAgilityPack;
using ClunkerBot.Models.ReturnModels.MessageReturnModels;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    public class OBDCode : CommandsBase
    {
        public static readonly string autoCodesBaseUrl = "https://www.autocodes.com";

        public static string Get(string code) {
            try {
                var document = ScrapeAutoCodesCodePage(code);

                //var test = document.SelectNodes("//h4")[0].

                string result = $@"<header>Possible Causes</header>
...

<header>Possible Symptoms</header>
...

<header>Description</header>
...";

                return BuildOutput(result, "Get OBDII Code", "🔌");
            } catch (Exception e) {
                return BuildErrorOutput(e);
            }
        }

        private static HtmlNode ScrapeAutoCodesCodePage(string code) {
            var url = autoCodesBaseUrl + "/" + code.ToLower() + ".html";
            var web = new HtmlWeb();
            var document = web.Load(url);

            return document.DocumentNode;
        }
    }
}