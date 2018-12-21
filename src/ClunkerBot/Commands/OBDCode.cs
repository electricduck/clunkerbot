using System;
using System.Collections.Generic;
using System.Linq;
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
                code = code.ToLower();

                var document = ScrapeAutoCodesCodePage(code);

                string autoCodesLink = "https://www.autocodes.com/" + code.ToLower() + ".html";
                string obdCodesLink = "https://www.obd-codes.com/" + code.ToLower();

                IEnumerable<HtmlNode> possibleCausesInfoPanelNodes = null;

                try {
                    possibleCausesInfoPanelNodes = document.SelectNodes("//div[@class='info']//*[text()[contains(., 'Possible causes')]]")[0]
                        .ParentNode.ChildNodes.Where(x => x.Name == "li");
                } catch {
                    string errorResult = $@"Unable to find code, or unable to scrape data.
                    
<header>Alternative Resources</header>
<subitem-bullet><a href='{autoCodesLink}'>AutoCodes</a></subitem-bullet>
<subitem-bullet><a href='{obdCodesLink}'>OBD-Codes</a></subitem-bullet>";

                    return BuildSoftErrorOutput(errorResult);
                }

                var possibleSymptomsInfoPanelNodes = document.SelectNodes("//div[@class='info']//*[text()[contains(., 'Possible symptoms')]]")[0]
                    .ParentNode.ChildNodes.Where(x => x.Name == "li");

                string possibleCauses = "";
                string possibleSymptoms = "";
                string description = "";

                foreach(var possibleCausesInfoPanelNode in possibleCausesInfoPanelNodes) {
                    var innerText = possibleCausesInfoPanelNode.InnerText;
                    try {
                        innerText = innerText
                            .Substring(0, innerText.IndexOf("&nbsp; What does this mean?"));
                    } catch {}
                    
                    possibleCauses += $"<subitem-bullet>{innerText}</subitem-bullet>";
                }

                foreach(var possibleSymptomsInfoPanelNode in possibleSymptomsInfoPanelNodes) {
                    var innerText = possibleSymptomsInfoPanelNode.InnerText;
                    try {
                        innerText = innerText
                            .Substring(0, innerText.IndexOf("&nbsp; What does this mean?"));
                    } catch {}
                    
                    possibleSymptoms += $"<subitem-bullet>{innerText}</subitem-bullet>";
                }

                if(String.IsNullOrEmpty(possibleCauses)) { possibleCauses = "<i>No Possible Causes</i>"; }
                if(String.IsNullOrEmpty(possibleSymptoms)) { possibleSymptoms = "<i>No Possible Symptoms</i>"; }
                if(String.IsNullOrEmpty(description)) { description = "<i>No Description</i>"; }


                string result = $@"<header>Possible Causes</header>
{possibleCauses}

<header>Possible Symptoms</header>
{possibleSymptoms}

See more at <a href='{autoCodesLink}'>AutoCodes</a> or <a href='{obdCodesLink}'>OBD-Codes</a>.
<footnote><i>Data from </i><a href='https://www.autocodes.com/'>AutoCodes</a><i>. Fair usage assumed.</i></footnote>";

                return BuildOutput(result, "Get OBDII Code", "🔌", code.ToUpper());
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