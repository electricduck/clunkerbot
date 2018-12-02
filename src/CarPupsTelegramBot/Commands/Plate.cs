using System;
using Telegram.Bot.Args;
using CarPupsTelegramBot.Api;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels;
using CarPupsTelegramBot.Utilities.PlateUtilities;

namespace CarPupsTelegramBot.Commands
{
    public class Plate
    {
        public static string Parse(string plate, string country)
        {
            try {
                PlateReturnModel plateReturn;

                country = country.ToLower();
                plate = plate.ToUpper();

                switch(country) {
                    case "gb":
                        plateReturn = GbPlateUtilities.ParseGbPlate(plate);
                        break;
                    default:
                        return HelpData.GetHelp("parseplate", true);
                }

                string yearString;

                if(plateReturn.Year == 0) {
                    yearString = "<i>Unknown</i>";
                } else {
                    yearString = plateReturn.Year.ToString();
                }

                string output = $@"#️⃣ <i>Parse Plate:</i> <code>{plate}</code>
—
";

                if(plateReturn.Format == Enums.GbPlateFormat.yr1902) {
                    var specialOutput = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlatePost2001Type.LordMayorOfLondon) {
                        specialOutput = "Lord Mayor of London";
                    } else if(plateReturn.Type == Enums.GbPlatePost2001Type.LordProvostsOfAberdeen) {
                        specialOutput = "Lord Provosts Of Aberdeen";
                    } else if(plateReturn.Type == Enums.GbPlatePost2001Type.LordProvostsOfEdinburgh) {
                        specialOutput = "Lord Provosts of Edinburgh";
                    } else if(plateReturn.Type == Enums.GbPlatePost2001Type.LordProvostsOfGlasgow) {
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

                    if(plateReturn.Type == Enums.GbPlatePost2001Type.QPlate) {
                        specialOutput = "Q Plate";
                    }

                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString}
<b>Special:</b> {specialOutput}
<b>Format:</b> Prefix <i>(1983 to 2001)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.current) {
                    var specialOutput = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlatePost2001Type.Reserved) {
                        specialOutput = "Reserved";
                    } else if(plateReturn.Type == Enums.GbPlatePost2001Type.Export) {
                        specialOutput = "Personal Export";
                    }
                    
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString} ({plateReturn.Month})
<b>Special:</b> {specialOutput}
<b>Format:</b> Current <i>(2001 to 2051)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.custom) {
                    output += $@"<i>This plate is a non-standard private plate. Check with DVLA records to find out more.</i>";
                }

                return output;
            } catch {
                return HelpData.GetHelp("parseplate", true);
            }
        }
    }
}