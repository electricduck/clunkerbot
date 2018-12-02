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

                string output = $@"#️⃣ <i>Parse Plate:</i> <code>{plate}</code>
—
";

                if(plateReturn.Format == Enums.GbPlateFormat.yr1902) {
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Issue No.:</b> {plateReturn.Issue}
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
<b>Year Reg.:</b> {plateReturn.Year}
<b>Format:</b> Suffix <i>(1963 to 1982)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.prefix) {
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {plateReturn.Year}
<b>Format:</b> Prefix <i>(1983 to 2001)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.current) {
                    var specialOutput = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlatePost2001Type.Reserved) {
                        specialOutput = "Reserved";
                    } else if(plateReturn.Type == Enums.GbPlatePost2001Type.Export) {
                        specialOutput = "Personal Export";
                    }
                    
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {plateReturn.Year} ({plateReturn.Month})
<b>Special:</b> {specialOutput}
<b>Format:</b> Current <i>(2001 to 2051)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.custom) {
                    output += $@"<i>This plate is a non-standard custom plate. Check with DVLA records to find out more.";
                }

//Year: {plateReturn.Year} ({plateReturn.Month})
//Location: {plateReturn.Location}
//Issue: {plateReturn.Issue}
//Type: {plateReturn.Type}
//Format: {plateReturn.Format}";

                return output;
            } catch {
                return HelpData.GetHelp("parseplate", true);
            }
        }
    }
}