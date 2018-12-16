using System;
using HtmlAgilityPack;
using Telegram.Bot.Args;
using CarPupsTelegramBot.Api;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels;
using CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels;
using CarPupsTelegramBot.Utilities.PlateUtilities;

namespace CarPupsTelegramBot.Commands
{
    public class Plate
    {
        public static string Parse(string plate, string country)
        {
            try {
                country = country.ToLower().Substring(0, 2);
                plate = plate.ToUpper();

                switch(country) {
                    case "de":
                        var parsedDePlate = ParseDePlate(plate);
                        return parsedDePlate.Message;
                    case "gb":
                        var parsedGbPlate = ParseGbPlate(plate);
                        return parsedGbPlate.Message;
                    case "gg":
                        var parsedGgPlate = ParseGgPlate(plate);
                        return parsedGgPlate.Message;
                    case "nl":
                        var parsedNlPlate = ParseNlPlate(plate);
                        return parsedNlPlate.Message;
                    default:
                        return $@"#️⃣ <i>Parse Plate:</i> ❓ <code>{plate}</code>
—
<i>Country code '{country}' is currenty unsupported.</i>";
                }
            } catch {
                return HelpData.GetHelp("parseplate", false);
            }
        }

        private static ParsedPlateMessageReturnModel ParseDePlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            DePlateReturnModel plateReturn = DePlateUtilities.ParseDePlate(plate);
        
            string output = $@"#️⃣ <i>Parse Plate:</i> 🇩🇪 <code>{plate}</code>
—
";

            string locationString;
            string specialString;

            if(plateReturn.Valid) {
                if(plateReturn.Format == Enums.DePlateFormat.yr1956) {
                    if(String.IsNullOrEmpty(plateReturn.Location)) {
                        locationString = "<i>Unknown</i>";
                    } else {
                        locationString = plateReturn.Location;
                    }

                    if(String.IsNullOrEmpty(plateReturn.Special)) {
                        specialString = "<i>No</i>";
                    } else {
                        specialString = plateReturn.Special;
                    }

                    output += $@"<b>Location:</b> {locationString}
<b>Special:</b> {specialString}
<b>Format:</b> Current <i>(1956 onwards)</i>";
                } else if(plateReturn.Format == Enums.DePlateFormat.diplomatic1956) {
                    output += $@"Format: Diplomatic
                    
<i>This is a diplomatic plate, found on cars used by foreign embassies, high commissions, consulates and international organisations. The cars themselves are usually not personally owned.</i>";
                } 
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported German plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "de";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseGbPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            PlateReturnModel plateReturn = GbPlateUtilities.ParseGbPlate(plate, Enums.GbPlateFormat.unspecified);

            string output = $@"#️⃣ <i>Parse Plate:</i> 🇬🇧 <code>{plate}</code>
—
";

            string yearString;

            if(plateReturn.Year == 0) {
                yearString = "<i>Unknown</i>";
            } else {
                yearString = plateReturn.Year.ToString();
            }

            if(plateReturn.Valid) {
                if(plateReturn.Format == Enums.GbPlateFormat.yr1902) {
                    var specialString = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlateSpecial.LordMayorOfLondon) {
                        specialString = "Lord Mayor of London";
                    } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfAberdeen) {
                        specialString = "Lord Provosts Of Aberdeen";
                    } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfEdinburgh) {
                        specialString = "Lord Provosts of Edinburgh";
                    } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfGlasgow) {
                        specialString = "Lord Provosts of Glasgow";
                    }

                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Issue No.:</b> {plateReturn.Issue}
<b>Special:</b> {specialString}
<b>Format:</b> 1902 to 1932";
                } else if(plateReturn.Format == Enums.GbPlateFormat.yr1932) {
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Issue No.:</b> {plateReturn.Issue}
<b>Format:</b> 1932 to 1963";
                } else if(plateReturn.Format == Enums.GbPlateFormat.yr1953) {
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Issue No.:</b> {plateReturn.Issue}
<b>Format:</b> 1932 to 1963 (Reversed)";
                } else if(plateReturn.Format == Enums.GbPlateFormat.suffix) {
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString}
<b>Format:</b> Suffix <i>(1963 to 1982)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.prefix) {
                    var specialString = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlateSpecial.QPlate) {
                        specialString = "Q Plate";
                    }

                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Prefix <i>(1983 to 2001)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.current) {
                    var specialString = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlateSpecial.Reserved) {
                        specialString = "Reserved";
                    } else if(plateReturn.Type == Enums.GbPlateSpecial.Export) {
                        specialString = "Personal Export";
                    }
                    
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString} ({plateReturn.Month})
<b>Special:</b> {specialString}
<b>Format:</b> Current <i>(2001 to 2051)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.trade2015) {
                    output += $@"<b>Issue:</b> {plateReturn.Issue}
<b>Format:</b> Trade <i>(2015 onwards)</i>

<i>This is a trade plate (2015 onwards), licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.diplomatic1979) {
                    output += $@"<b>Diplomatic Org.:</b> {plateReturn.DiplomaticOrganisation}
<b>Diplomatic Type:</b> {plateReturn.DiplomaticType}
<b>Diplomatic Rank:</b> {plateReturn.DiplomaticRank}
<b>Format:</b> Diplomatic

<i>This is a diplomatic plate, found on cars used by foreign embassies, high commissions, consulates and international organisations. The cars themselves are usually not personally owned.</i>";
                } 
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Great Britain plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "gb";
            //parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }
    
        private static ParsedPlateMessageReturnModel ParseGgPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            GgPlateReturnModel plateReturn = GgPlateUtilities.ParseGgPlate(plate);

            string output = $@"#️⃣ <i>Parse Plate:</i> 🇬🇬 <code>{plate}</code>
—
";

            if(plateReturn.Valid) {
                output += $"<b>Issue:</b> {plate}";
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Guersney plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "gg";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseNlPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            NlPlateReturnModel plateReturn = NlPlateUtilities.ParseNlPlate(plate);
        
            string output = $@"#️⃣ <i>Parse Plate:</i> 🇳🇱 <code>{plate}</code>
—
";

            string specialString;
            string yearString;

            if(String.IsNullOrEmpty(plateReturn.Special)) {
                specialString = "<i>No</i>";
            } else {
                specialString = plateReturn.Special;
            }

            if(String.IsNullOrEmpty(plateReturn.Year)) {
                yearString = "<i>Unknown</i>";
            } else {
                yearString = plateReturn.Year;
            }

            if(plateReturn.Valid) {
                if(plateReturn.Format == Enums.NlPlateFormat.yr1898) {
                    output += $@"<b>Location:</b> {plateReturn.Location}
<b>Issue:</b> {plateReturn.Issue}
<b>Format:</b> 1989 to 1951";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1951) {
                    output += $@"<b>Format:</b> Side Code 1 <i>(1951 to 1956)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1965) {
                    output += $@"<b>Format:</b> Side Code 2 <i>(1965 to 1973)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1973) {
                    output += $@"<b>Format:</b> Side Code 3 <i>(1973 to 1978)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1978) {
                    output += $@"<b>Format:</b> Side Code 4 <i>(1978 to 1991)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1991) {
                    output += $@"<b>Format:</b> Side Code 5 <i>(1991 to 1999)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1999) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 6 <i>(1999 to 2008)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2006) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 7 <i>(2006 onwards)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2006b) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 8 <i>(2006 onwards)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2006c) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 9 <i>(2006 onwards)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2011) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 10 <i>(2011 to 2015)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2015) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 11 <i>(2015 onwards)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2016) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 13 <i>(2016 onwards)</i>";
                }
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Netherlands plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "nl";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }
    }
}