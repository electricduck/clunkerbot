using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels;

namespace CarPupsTelegramBot.Utilities.PlateUtilities
{
    public class UsOhPlateUtilities
    {
        private static string Year2004Regex = @"^(([A-Z]{3})([1000-9999]{4}))$";
        private static string Year2015BikeRegex = @"^(([A-Z]{3})([01-99]{2}))$";
        private static string DUIOffendersRegex = @"^([0-9]{6,7})$";
    
        public static UsOhPlateReturnModel ParseUsOhPlate(string plate)
        {
            UsOhPlateReturnModel plateReturn = null;

            plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Year2004Regex)) {
                plateReturn = ParseUsOhYr2013Plate(plate);
            } else if(Regex.IsMatch(plate, Year2015BikeRegex)) {
                plateReturn = ParseUsOhYr2015BikePlate(plate);
            } else if(Regex.IsMatch(plate, DUIOffendersRegex)) {
                plateReturn = ParseUsOhDUIOffendersPlate(plate);
            } else {
                plateReturn = new UsOhPlateReturnModel {
                    Valid = false
                };
            }

            return plateReturn;
        }

        public static UsOhPlateReturnModel ParseUsOhYr2013Plate(string plate)
        {
            Regex regex = new Regex(Year2004Regex);
            Match match = regex.Match(plate);

            UsOhPlateReturnModel returnModel = new UsOhPlateReturnModel {
                Format = Enums.UsOhPlateFormat.yr2004,
                Valid = true
            };

            string mnemonic = match.Groups[2].Value;

            returnModel.Special = GetReservedSeriesMnemonic(mnemonic);

            return returnModel;
        }

        public static UsOhPlateReturnModel ParseUsOhYr2015BikePlate(string plate)
        {
            Regex regex = new Regex(Year2015BikeRegex);
            Match match = regex.Match(plate);

            UsOhPlateReturnModel returnModel = new UsOhPlateReturnModel {
                Format = Enums.UsOhPlateFormat.yr2015Bike,
                Valid = true
            };

            if(plate.StartsWith("Z")) {
                returnModel.Special = "Veteran Motorcycle";
            }

            return returnModel;
        }

        public static UsOhPlateReturnModel ParseUsOhDUIOffendersPlate(string plate)
        {
            Regex regex = new Regex(DUIOffendersRegex);
            Match match = regex.Match(plate);

            UsOhPlateReturnModel returnModel = new UsOhPlateReturnModel {
                Format = Enums.UsOhPlateFormat.duiOffender,
                Issue = match.Groups[2].Value,
                Valid = true
            };

            return returnModel;
        }

        private static string GetReservedSeriesMnemonic(string reservedSeriesMnemonics)
        {
            if(ReservedSeriesMnemonics.ContainsKey(reservedSeriesMnemonics)) {
                string reservedSeries;

                ReservedSeriesMnemonics.TryGetValue(reservedSeriesMnemonics, out reservedSeries);

                return reservedSeries;
            } else {
                return "";
            }
        }

        private static Dictionary<string, string> ReservedSeriesMnemonics = new Dictionary<string, string>()
        {
            {"FAC", "First Automotive Corp., Cincinnati"},
            {"GLR", "Grand Leasing and Sales"},
            {"GAN", "Ganley Automotive Lease"},
            {"HON", "Honda"},
            {"HOM", "Honda of Mentor"},
            {"JAY", "Jay Auto Group, Bedford"},
            {"JSL", "Jake Sweeney Leasing, Cincinnati"},
            {"LAS", "Shaker Auto Leasing"},
            {"LEX", "Metro Lexus"},
            {"LSX", "Matro Lexus"},
            {"MAL", "Mike Albert Resale Center and Leasing, Cincinnati"},
            {"MBZ", "Mercedes-Benz"},
            {"MCT", "Motorcars Toyota, Cleveland Heights"},
            {"MCH", "Motorcars Toyota, Cleveland Heights"},
            {"MET", "Metro Toyota, Cleveland"},
            {"MGM", "Marshall Goldman Motors"},
            {"MKB", "MKB Leasing, Marietta"},
            {"MVP", "Classic Auto Group, Cleveland"},
            {"NON", "Nissan of North Olmsted"},
            {"SUN", "Sunnyside, Cleveland"},
            {"SSA", "Sunnyside Audi"},
            {"SSH", "Sunnyside Honda"},
            {"SST", "Sunnyside Toyota"},
            {"TOB", "Toyota of Bedford"},
            {"TOY", "Toyota"},
            {"VCJ", "Adventure Chrysler Jeep, Willoughby"},
            {"WIN", "Classic Auto Group, Cleveland"}
        };
    }
}