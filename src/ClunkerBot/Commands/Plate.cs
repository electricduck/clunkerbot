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

            string diplomaticInfoString = "Found on vehicles used by foreign embassies, high commissions, consulates and international organisations. The vehicles themselves are usually not personally owned.";

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
                        string plateInfo_InspectionPeriod = (string)plateInfo["inspectionPeriod"];
                        string plateInfo_Issue = (string)plateInfo["issue"];
                        string plateInfo_Region = (string)plateInfo["region"];
                        string plateInfo_RegistrationYear = (string)plateInfo["registrationYear"];
                        string plateInfo_Series = (string)plateInfo["series"];
                        string plateInfo_Special = (string)plateInfo["special"];
                        string plateInfo_VehicleType = (string)plateInfo["vehicleType"];

                        if(plateInfo_Diplomatic.Count() > 0)
                        {
                            plateInfo_Diplomatic_Organisation = (string)plateInfo_Diplomatic["organisation"];
                            plateInfo_Diplomatic_Rank = (string)plateInfo_Diplomatic["rank"];
                            plateInfo_Diplomatic_Type = (string)plateInfo_Diplomatic["type"];
                        }

                        string info = null;
                        string issue = null;
                        string year = plateInfo_RegistrationYear;

                        string inspectionPeriodString = "Inspection Period";
                        string issueString = "Issue";
                        string regionString = "Region";
                        string yearString = "Year";

                        if(plateInfo_Issue != null && plateInfo_Series != null)
                        {
                            issue = $"{plateInfo_Issue} [{plateInfo_Series}]";
                        }
                        else
                        {
                            issue = $"{plateInfo_Issue}";
                        }

                        switch(plateInfo_FormatEnum)
                        {
                            case 4:
                                regionString = "Reg. Office";
                                break;
                            case 5:
                                issueString = "Issue No.";
                                break;
                            case 6:
                                regionString = "Reg. Office";
                                break;
                            case 10:
                                regionString = "Reg. Department";
                                break;
                            case 11:
                                info = "* This is based on a steady average amount of cars per year, providing the SIV system lasts the estimated 80 years initially planned.";
                                issue = $"{plateInfo_Issue} (approx.)";
                                year = $"{plateInfo_RegistrationYear} (approx.)";
                                issueString = "Issue No.";
                                break;
                            case 25:
                                info = "Region is only valid for vehicles registered between 1991 and 2004.";
                                break;
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                                issueString = "Issue No.";
                                regionString = "DVLA Office";
                                break;
                            case 31:
                                info = "Licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.";
                                break;
                            case 32:
                                info = diplomaticInfoString;
                                break;
                            case 34:
                                info = "Issued to Japanese citizens for internationl travel — the Japanese writing system is considered unacceptable outside of Japan, as they are not easily identifiable to local authorities.";
                                break;
                            case 36:
                                info = "Temporary plate used for vehicles imported and exported to/from Lithuania, only valid for 90 days.";
                                break;
                            case 37:
                                info = "Licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.";
                                break;
                            case 38:
                                info = diplomaticInfoString;
                                break;
                            case 39:
                                info = "Found on taxis and private-hire vehicles.";
                                break;
                            case 40:
                                info = "Found on vehicles used in the military for transport on public roads";
                                break;
                            case 41:
                                info = "Northern Ireland, although part of Great Britain, has its own plate format.";
                                issueString = "Issue No.";
                                break;
                            case 44:
                                info = diplomaticInfoString;
                                break;
                            case 48:
                                info = "Temporary plate used for vehicles exported to/from Finland.";
                                break;
                            case 49:
                                info = diplomaticInfoString;
                                break;
                            case 50:
                                yearString = "Registration Year";
                                break;
                                
                        }

                        if(plate.ToUpper() == "1420H")
                        {
                            info = "Ah, a Reliant Scimitar. Princess Anne owns this one, you know.";
                        }

                        result += !String.IsNullOrEmpty(plateInfo_Region) ? RenderDetailLine(regionString, plateInfo_Region) : String.Empty;
                        result += !String.IsNullOrEmpty(year) ? RenderDetailLine(yearString, year) : String.Empty;
                        result += !String.IsNullOrEmpty(plateInfo_InspectionPeriod) ? RenderDetailLine(inspectionPeriodString, plateInfo_InspectionPeriod) : String.Empty;
                        result += issue != "0" ? RenderDetailLine(issueString, issue) : String.Empty;
                        result += !String.IsNullOrEmpty(plateInfo_VehicleType) ? RenderDetailLine("Vehicle Type", plateInfo_VehicleType) : String.Empty;
                        result += !String.IsNullOrEmpty(plateInfo_Special) ? RenderDetailLine("Special", plateInfo_Special) : String.Empty;
                        result += !String.IsNullOrEmpty(plateInfo_Diplomatic_Organisation) ? RenderDetailLine("Diplomatic Org.", plateInfo_Diplomatic_Organisation) : String.Empty;
                        result += !String.IsNullOrEmpty(plateInfo_Diplomatic_Rank) ? RenderDetailLine("Diplomatic Rank", plateInfo_Diplomatic_Rank) : String.Empty;
                        result += !String.IsNullOrEmpty(plateInfo_Diplomatic_Type) ? RenderDetailLine("Diplomatic Type", plateInfo_Diplomatic_Type) : String.Empty;

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
            if(content == "No")
            {
                content = "<i>No</i>";
            }
            else if(content == "Unknown")
            {
                content = "<i>Unknown</i>";
            }

            content = content
                .ToString()
                .Replace("(", "<i>(")
                .Replace(")", ")</i>")
                .Replace("[", "(")
                .Replace("]", ")");

            return $"<b>{name}:</b> {content}{Environment.NewLine}";
        }

        private static string RenderParsedPlate(string flag, string plate)
        {
            return $"{flag} {plate}";
        }
    }
}