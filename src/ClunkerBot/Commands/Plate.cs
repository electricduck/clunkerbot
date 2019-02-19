using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Args;
using ClunkerBot.Api;
using ClunkerBot.Data;
using ClunkerBot.Models;
using ClunkerBot.Models.ReturnModels;
using ClunkerBot.Services;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    public class Plate : CommandsBase
    {
        public static string Parse(string plate, string country = "any")
        {
            PlateWtfApiService _plateWtfApiService = new PlateWtfApiService();

            string outputArgument = $"❓ {plate}";
            string outputEmoji = "#️⃣";
            string outputHeader = "Parse Plate";

            try
            {
                string result = "";

                string jsonResult = _plateWtfApiService.GetPlate(plate, country).Result;
                JObject parsedJson = JObject.Parse(jsonResult);

                var matchedPlates = parsedJson["plates"];
                int matchedPlatesCount = parsedJson["plates"].Count();

                if(matchedPlatesCount == 1)
                {
                    var firstPlateJson = parsedJson["plates"][0];

                    bool valid = (bool)firstPlateJson["valid"];
                    var parsedPlate = (string)firstPlateJson["parsed"];

                    var plateCountry = firstPlateJson["country"];
                    var plateInfo = firstPlateJson["info"];

                    string plateCountry_Flag = (string)plateCountry["flag"];
                    string plateCountry_Name = (string)plateCountry["name"];

                    if(valid)
                    {
                        var plateInfo_Diplomatic = plateInfo["diplomatic"];
                        string plateInfo_Diplomatic_Organisation = "";
                        string plateInfo_Diplomatic_Rank = "";
                        string plateInfo_Diplomatic_Type = "";
                        string plateInfo_Format = (string)plateInfo["format"];
                        int plateInfo_FormatEnum = (int)plateInfo["formatEnum"];
                        string plateInfo_Issue = (string)plateInfo["issue"];
                        string plateInfo_Region = (string)plateInfo["region"];
                        string plateInfo_RegistrationYear = (string)plateInfo["registrationYear"];
                        string plateInfo_Special = (string)plateInfo["special"];
                        string plateInfo_VehicleType = (string)plateInfo["vehicleType"];

                        if(plateInfo_Diplomatic.Count() > 0)
                        {
                            plateInfo_Diplomatic_Organisation = (string)plateInfo_Diplomatic["organisation"];
                            plateInfo_Diplomatic_Rank = (string)plateInfo_Diplomatic["rank"];
                            plateInfo_Diplomatic_Type = (string)plateInfo_Diplomatic["type"];
                        }

                        string info = null;

                        switch(plateInfo_FormatEnum)
                        {
                            // AL
                            case 42:
                                break;
                            case 43:
                                result += RenderDetailLine("Region", plateInfo_Region);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;
                            case 44:
                                result += RenderDetailLine("Diplomatic Org.", plateInfo_Diplomatic_Organisation);
                                info = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";
                                break;

                            // AT
                            case 6:
                                result += RenderDetailLine("Reg. Office", plateInfo_Region);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;

                            // DE
                            case 7:
                                result += RenderDetailLine("Region", plateInfo_Region);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;

                            // FR
                            case 10:
                                result += RenderDetailLine("Reg. Department", plateInfo_Region);
                                break;
                            case 11:
                                result += RenderDetailLine("Issue No.", $"{plateInfo_Issue} (approx.)");
                                result += RenderDetailLine("Year", $"{plateInfo_RegistrationYear} (approx.)*");
                                info = "* This is based on a steady average amount of cars per year, providing the SIV system lasts the estimated 80 years initially planned.";
                                break;

                            // GB
                            case 25:
                            case 26:
                            case 27:
                                result += RenderDetailLine("DVLA Office", plateInfo_Region);
                                result += RenderDetailLine("Issue No.", plateInfo_Issue);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;
                            case 28:
                            case 29:
                            case 30:
                                result += RenderDetailLine("DVLA Office", plateInfo_Region);
                                result += RenderDetailLine("Year", plateInfo_RegistrationYear);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;
                            case 31:
                                result += RenderDetailLine("Issue No.", plateInfo_Issue);
                                info = "Licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.";
                                break;
                            case 32:
                                result += RenderDetailLine("Diplomatic Org.", plateInfo_Diplomatic_Organisation);
                                result += RenderDetailLine("Diplomatic Type", plateInfo_Diplomatic_Type);
                                result += RenderDetailLine("Diplomatic Rank", plateInfo_Diplomatic_Rank);
                                info = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";
                                break;

                            // GB-NIR
                            case 41:
                                result += RenderDetailLine("Region", plateInfo_Region);
                                result += RenderDetailLine("Issue No.", plateInfo_Issue);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                info = "Northern Ireland, although part of Great Britain, has its own plate format.";
                                break;

                            // GG
                            case 5:
                                result += RenderDetailLine("Issue No.", plateInfo_Issue);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;

                            // JP
                            case 34:
                                result += RenderDetailLine("Region", plateInfo_Region);
                                result += RenderDetailLine("Vehicle", plateInfo_VehicleType);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                info = "Issued to Japanese citizens for internationl travel — the Japanese writing system is considered unacceptable outside of Japan, as they are not easily identifiable to local authorities.";
                                break;
                            case 33: // TODO: Fix Japanese chars not being sent
                                result += RenderDetailLine("Region", plateInfo_Region);
                                result += RenderDetailLine("Vehicle", plateInfo_VehicleType);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;

                            // LT
                            case 35:
                                result += RenderDetailLine("Region", plateInfo_Region);
                                break;
                            case 36:
                                info = "Temporary plate used for vehicles imported and exported to/from Lithuania, only valid for 90 days.";
                                break;
                            case 37:
                                info = "Licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.";
                                break;
                            case 38:
                                result += RenderDetailLine("Diplomatic Org.", plateInfo_Diplomatic_Organisation);
                                info = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";
                                break;
                            case 39:
                                info = "Found on taxis and private-hire vehicles.";
                                break;
                            case 40:
                                info = "Found on vehicles used in the military for transport on public roads";
                                break;

                            // NL
                            case 12:
                                result += RenderDetailLine("Region", plateInfo_Region);
                                break;
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                            case 22:
                            case 23:
                            case 24:
                                result += RenderDetailLine("Year", plateInfo_RegistrationYear);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;

                            // RU
                            case 4:
                                result += RenderDetailLine("Reg. Office", plateInfo_Region);
                                result += RenderDetailLine("Special", plateInfo_Special);
                                break;
                        }

                        outputArgument = RenderParsedPlate(plateCountry_Flag, parsedPlate);
                        result += RenderDetailLine("Format", plateInfo_Format, false);
                    
                        if(!String.IsNullOrEmpty(info))
                        {
                            result += $"{Environment.NewLine}<i>{info}</i>";
                        }
                    }
                    else
                    {
                        result = $"<i>This is an invalid, custom/private, or unsupported plate for {plateCountry_Name}. Contact</i> @theducky <i>if you believe it is a valid format.</i>";
                    }
                }
                else if(matchedPlatesCount == 0)
                {
                    result = @"<i>No match for plate found.</i>
<i>The country may not be supported — check the help (</i><code>/help parseplate</code><i>) for a list of supported countries.</i>";
                }
                else
                {
                    string multipleMatchesMessage = "";

                    foreach(var matchedPlate in matchedPlates) {
                        string matchedPlate_CountryCode = (string)matchedPlate["country"]["code"];
                        string matchedPlate_CountryFlag = (string)matchedPlate["country"]["flag"];
                        string matchedPlate_Plate = (string)matchedPlate["parsed"];

                        multipleMatchesMessage += $@"<li> <b>{matchedPlate_CountryFlag} {matchedPlate_CountryCode.ToUpper()}:</b> <code>/parseplate {matchedPlate_Plate} {matchedPlate_CountryCode.ToLower()}</code></li>{Environment.NewLine}";
                    }

                    result = $@"<b>Multiple matches found</b>
{multipleMatchesMessage}
<i>Type one of the commands to specify the country.</i>";
                }

                return BuildOutput(result, outputHeader, outputEmoji, outputArgument);
            }
            catch (Exception e)
            {
                return BuildErrorOutput(e);
            }
        }

        private static string RenderDetailLine(string name, string content, bool newLine = true)
        {
            if(content == null)
            {
                if(name == "Special")
                {
                    content = "<i>No</i>";
                }
                else
                {
                    content = "<i>Unknown</i>";
                }
            }

            content = content
                .ToString()
                .Replace("(", "<i>(")
                .Replace(")", ")</i>");

            return $"<b>{name}:</b> {content}{Environment.NewLine}";
        }

        private static string RenderParsedPlate(string flag, string plate)
        {
            return $"{flag} {plate}";
        }
    }
}