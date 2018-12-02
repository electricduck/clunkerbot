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
        public static string Parse(string plate, string location)
        {
            try {
                PlateReturnModel plateReturn;

                switch(location) {
                    case "gb":
                        plateReturn = GbPlateUtilities.ParseGbPlate(plate);
                        break;
                    default:
                        return HelpData.GetHelp("parseplate", true);
                }

                string output = $@"Year: {plateReturn.Year}
Location: {plateReturn.Location}
Issue: {plateReturn.Issue}
Type {plateReturn.Format}";

                return output;
            } catch {
                return HelpData.GetHelp("parseplate", true);
            }
        }
    }
}