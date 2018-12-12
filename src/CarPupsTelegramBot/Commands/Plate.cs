using System;
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
                    case "gb":
                        return ParseGbPlate(plate);
                    case "gg":
                        return ParseGgPlate(plate);
                    default:
                        return $@"#️⃣ <i>Parse Plate:</i> ❓ <code>{plate}</code>
—
<i>Country code '{country}' is currenty unsupported.</i>";
                }
            } catch {
                return HelpData.GetHelp("parseplate", true);
            }
        }

        private static string ParseGbPlate(string plate)
        {
            PlateReturnModel plateReturn = GbPlateUtilities.ParseGbPlate(plate, Enums.GbPlateFormat.unspecified);

            string yearString;

            if(plateReturn.Year == 0) {
                yearString = "<i>Unknown</i>";
            } else {
                yearString = plateReturn.Year.ToString();
            }

            string output = $@"#️⃣ <i>Parse Plate:</i> 🇬🇧 <code>{plate}</code>
—
";

            if(plateReturn.Format == Enums.GbPlateFormat.yr1902) {
                var specialOutput = "<i>No</i>";

                if(plateReturn.Type == Enums.GbPlateSpecial.LordMayorOfLondon) {
                    specialOutput = "Lord Mayor of London";
                } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfAberdeen) {
                    specialOutput = "Lord Provosts Of Aberdeen";
                } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfEdinburgh) {
                    specialOutput = "Lord Provosts of Edinburgh";
                } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfGlasgow) {
                    specialOutput = "Lord Provosts of Glasgow";
                }

                output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Issue No.:</b> {plateReturn.Issue}
<b>Special:</b> {specialOutput}
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
                var specialOutput = "<i>No</i>";

                if(plateReturn.Type == Enums.GbPlateSpecial.QPlate) {
                    specialOutput = "Q Plate";
                }

                output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString}
<b>Special:</b> {specialOutput}
<b>Format:</b> Prefix <i>(1983 to 2001)</i>";
            } else if(plateReturn.Format == Enums.GbPlateFormat.current) {
                var specialOutput = "<i>No</i>";

                if(plateReturn.Type == Enums.GbPlateSpecial.Reserved) {
                    specialOutput = "Reserved";
                } else if(plateReturn.Type == Enums.GbPlateSpecial.Export) {
                    specialOutput = "Personal Export";
                }
                
                output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString} ({plateReturn.Month})
<b>Special:</b> {specialOutput}
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
            } else if(plateReturn.Format == Enums.GbPlateFormat.custom) {
                output += $@"<i>This plate is a non-standard private plate. Check with DVLA records to find out more.</i>";
            }

            return output;
        }
    
        private static string ParseGgPlate(string plate)
        {
            GgPlateReturnModel plateReturn = GgPlateUtilities.ParseGgPlate(plate);

            string output = $@"#️⃣ <i>Parse Plate:</i> 🇬🇬 <code>{plate}</code>
—
";

            if(plateReturn.Valid) {
                output += $"<b>Issue:</b> {plate}";
            } else {
                output += "<i>This is an invalid Guersney plate.</i>";
            }

            return output;
        }
    }
}