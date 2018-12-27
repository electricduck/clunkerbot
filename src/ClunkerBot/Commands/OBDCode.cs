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
                    
<h2>Alternative Resources</h2>
<li><a href='{autoCodesLink}'>AutoCodes</a></li>
<li><a href='{obdCodesLink}'>OBD-Codes</a></li>";

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
                    
                    possibleCauses += $"<li>{innerText}</li>";
                }

                foreach(var possibleSymptomsInfoPanelNode in possibleSymptomsInfoPanelNodes) {
                    var innerText = possibleSymptomsInfoPanelNode.InnerText;
                    try {
                        innerText = innerText
                            .Substring(0, innerText.IndexOf("&nbsp; What does this mean?"));
                    } catch {}
                    
                    possibleSymptoms += $"<li>{innerText}</li>";
                }

                if(String.IsNullOrEmpty(possibleCauses)) { possibleCauses = "<i>No Possible Causes</i>"; }
                if(String.IsNullOrEmpty(possibleSymptoms)) { possibleSymptoms = "<i>No Possible Symptoms</i>"; }
                if(String.IsNullOrEmpty(description)) { description = "<i>No Description</i>"; }

                var seeMoreLinks = new Dictionary<string, string> {
                    {autoCodesLink, "AutoCodes"},
                    {obdCodesLink, "OBD-Codes"}
                };
                var seeMoreString = BuildSeeMoreString(seeMoreLinks);

                string result = $@"<h2>Possible Causes</h2>
{possibleCauses}

<h2>Possible Symptoms</h2>
{possibleSymptoms}

{seeMoreString}
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