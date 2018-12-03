using System;
using System.Collections.Generic;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot.Commands
{
    public class Garage
    {
        public static string AddCarTo(
            UserModel user,
            string plate,
            string make = "?",
            string model = "?",
            string generation = "?",
            string trim = "?",
            string engineSize = "?",
            string engineType = "?",
            string engineFuel = "?",
            string transmission = "?",
            string driveType = "?",
            string engineCode = "?"
        )
        {
            try{
                GarageData garageData = new GarageData();
                UserData userData = new UserData();

                decimal parsedEngineSize;

                if(engineSize.Contains('c')) {
                    engineSize = engineSize.Replace("cc", "");
                    parsedEngineSize = Convert.ToDecimal(engineSize) / 1000;
                } else {
                    parsedEngineSize = Convert.ToDecimal(engineSize);
                }

                Enums.EngineTypeEnum parsedEngineType = engineType == "?" ? Enums.EngineTypeEnum.Unknown : (Enums.EngineTypeEnum)System.Enum.Parse(typeof(Enums.EngineTypeEnum), engineType, true);
                Enums.EngineFuelEnum parsedEngineFuel = engineFuel == "?" ? Enums.EngineFuelEnum.Unknown : (Enums.EngineFuelEnum)System.Enum.Parse(typeof(Enums.EngineFuelEnum), engineFuel, true);
                Enums.TransmissionEnum parsedTransmission = transmission == "?" ? Enums.TransmissionEnum.Unknown : (Enums.TransmissionEnum)System.Enum.Parse(typeof(Enums.TransmissionEnum), transmission, true);
                Enums.DriveTypeEnum parsedDriveType = driveType == "?" ? Enums.DriveTypeEnum.Unknown : (Enums.DriveTypeEnum)System.Enum.Parse(typeof(Enums.DriveTypeEnum), driveType, true);

                GarageModel garage = new GarageModel
                {
                    Plate = plate.ToUpper(),
                    Make = make,
                    Model = model,
                    Generation = generation,
                    Trim = trim,
                    EngineSize = parsedEngineSize,
                    EngineType = parsedEngineType,
                    EngineFuel = parsedEngineFuel,
                    Transmission = parsedTransmission,
                    DriveType = parsedDriveType,
                    EngineCode = engineCode,
                    TelegramUserId = user.TelegramId
                };

                garageData.AddUpdateGarageItem(garage);
                userData.AddUpdateUser(user);

                return "Car added.";
            } catch(Exception e) {
                return "oops";
            }
        }

        public static string Get(string username)
        {
            GarageData garageData = new GarageData();
            UserData userData = new UserData();

            username = username.Replace("@", "");

            UserModel telegramUser = userData.GetUserByTelegramUsername(username);
            List<GarageModel> cars = garageData.GetCarsForUser(telegramUser.TelegramId);

            string listOfVehicles = "";

            foreach(GarageModel car in cars)
            {
                ParseCarDetailsReturnModel parsedCarDetails = ParseCarDetails(car);
                listOfVehicles += $"<code>#{car.Id}</code> | <b>{car.Make} {car.Model}</b> {parsedCarDetails.ParsedGeneration} {car.EngineSize} {car.Trim}\r\n";
            }

            return $@"🚘 <i>Garage of</i> <b>@{username}</b>
—
{listOfVehicles}
<i>To see more details and images of a vehicle, do</i> <code>/getcar #123</code><i>.</i>";
        }

        public static string GetCarFrom(string plate)
        {
            GarageData garageData = new GarageData();

            GarageModel garage = null;

            if(plate.Substring(0, 1) == "#") {
                garage = garageData.GetCarFromGarageById(Convert.ToInt32(plate.Replace("#", "")));
            } else {
                garage = garageData.GetCarFromGarageByPlate(plate.ToUpper());
            }

            string userString = TelegramUtilities.GetTelegramUser(garage.TelegramUserId);
            ParseCarDetailsReturnModel parsedCarDetails = ParseCarDetails(garage);

            return $@"<b>{garage.Make} {garage.Model}</b> {parsedCarDetails.ParsedPlate}
—
<b>Gen.:</b> {parsedCarDetails.ParsedGeneration}
<b>Trim:</b> {garage.Trim}
<b>Engine:</b> {parsedCarDetails.ParsedEngine}
<b>Drive:</b> {garage.Transmission} {garage.DriveType}
<b>Owner:</b> {userString}"
                .Replace("Unknown", "<i>Unknown</i>")
                .Replace("?", "<i>Unknown</i>")
                .Replace("<i><i>", "<i>")
                .Replace("</i></i>", "</i>");
        }
    
        private static ParseCarDetailsReturnModel ParseCarDetails(GarageModel garage)
        {
            ParseCarDetailsReturnModel parseCarDetailsReturn = new ParseCarDetailsReturnModel();

            parseCarDetailsReturn.ParsedEngineSize = garage.EngineSize == 0 ? "<i>Unknown</i>" : garage.EngineSize.ToString();
            parseCarDetailsReturn.ParsedPlate = garage.Plate == "?" ? "" : $" | <code>{garage.Plate}</code>";

            string parsedEngineSize = parseCarDetailsReturn.ParsedEngineSize;

            if(garage.EngineFuel == Enums.EngineFuelEnum.Diesel) {
                parseCarDetailsReturn.ParsedEngine = $"{parsedEngineSize}D {garage.EngineType} <i>{garage.EngineCode}</i>";
            } else if(garage.EngineFuel == Enums.EngineFuelEnum.Electric) {
                parseCarDetailsReturn.ParsedEngine = $"{parsedEngineSize}kW {garage.EngineType} <i>{garage.EngineCode}</i>";
            } else {
                parseCarDetailsReturn.ParsedEngine = $"{parsedEngineSize} {garage.EngineType} <i>{garage.EngineCode}</i>";
            }

            parseCarDetailsReturn.ParsedGeneration = garage.Generation
                .Replace("(", "<i>[").Replace(")", "]</i>");

            return parseCarDetailsReturn;
        }
    }
}