using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using ClunkerBot.Services;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    public class WolframAlpha : CommandsBase
    {
        public static string Ask(string query)
        {
            WolframApiService _wolframApiService = new WolframApiService();

            string outputEmoji = "🤔";
            string outputHeader = "Ask";

            try
            {
                query = WebUtility.UrlEncode(query);

                string xmlResult = _wolframApiService.QueryApiAsync(query).Result;

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlResult);

                //foreach(XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)    

                string result = "";

                int resultsCount = xmlDocument.DocumentElement.SelectNodes("pod").Count;

                if(resultsCount == 0)
                {
                    query = WebUtility.UrlDecode(query);
                    result = $@"<i>Sorry, I don't understand your question.</i>";
                }
                else
                {
                    string interpretation = xmlDocument.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;
                    string mainResult = ParseResultOutput(xmlDocument.DocumentElement.ChildNodes[1].ChildNodes[0].SelectNodes("plaintext")[0].InnerText);
                    string mainResultType = "";
                    if(xmlDocument.DocumentElement.ChildNodes[1].ChildNodes[0].SelectNodes("microsources").Count != 0)
                    {
                        mainResultType = xmlDocument.DocumentElement.ChildNodes[1].ChildNodes[0].SelectNodes("microsources")[0].SelectNodes("microsource")[0].InnerText;
                    }
                    else
                    {
                        mainResultType = xmlDocument.DocumentElement.ChildNodes[1].Attributes[2].Value;
                    }
                    string mainResultTypeString = StringUtilities.AddSpacesToSentence(mainResultType, true);
                    string mainResultTypeIcon = GetResultTypeIcon(mainResultType);

                    string moreResultsLink = $"https://www.wolframalpha.com/input/?i={query}";

                    query = interpretation;

                    result = $@"{mainResult}
{mainResultTypeIcon} <i>{mainResultTypeString}</i>
<footnote><a href='{moreResultsLink}'>➡ More Results</a></footnote>";
                }

                return BuildOutput(result, outputHeader, outputEmoji, query);
            }
            catch(Exception e)
            {
                return BuildErrorOutput(e);
            }
        }

        private static string ParseResultOutput(string output)
        {
            output = output
                .Replace("(", "</b>(")
                .Replace(")", ")<b>");

            return $"<b>{output}</b>";
        }

        private static string GetResultTypeIcon(string code)
        {
            if(ResultTypeIcons.ContainsKey(code))
            {
                string decodedRegion;
                ResultTypeIcons.TryGetValue(code, out decodedRegion);
                return decodedRegion;
            }
            else
            {
                return "🔢";
            }
        }

        private static Dictionary<string, string> ResultTypeIcons = new Dictionary<string, string>()
        {
            {"BuildingData", "🗽"},
            {"CityData", "🏙️"},
            {"CommonSymbol", "🔣"},
            {"ConversionToOtherUnits", "🔢"},
            {"CountryData", "🌍"},
            {"DictionaryLookup", "📗"},
            {"FinancialData", "💵"},
            {"GivenNameData", "🧑"},
            {"NameData", "🧑"},
            {"Result", "🔢"},
            {"UnitConversion", "↔️"},
            {"WeatherData", "🌥️"},
            {"WordData", "🔤"}
        };
    }
}