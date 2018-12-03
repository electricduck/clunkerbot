using System;
using HtmlAgilityPack;
using CarPupsTelegramBot.Models.ReturnModels.MessageReturnModels;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot.Commands
{
    public class OBDCode
    {
        public static readonly string autoCodesBaseUrl = "https://www.autocodes.com";

        public static TextMessageReturnModel Get(string code) {
            try {
                var document = ScrapeAutoCodesCodePage(code);

                //var test = document.SelectNodes("//h4")[0].

                return null;
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
                return null;
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