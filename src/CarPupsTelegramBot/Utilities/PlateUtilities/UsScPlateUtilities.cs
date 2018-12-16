using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels;

namespace CarPupsTelegramBot.Utilities.PlateUtilities
{
    public class UsScPlateUtilities
    {
        private static string Year2008Regex = @"^(([A-Z]{3})([101-999]{3}))$";
    
        public static UsScPlateReturnModel ParseUsScPlate(string plate)
        {
            UsScPlateReturnModel plateReturn = null;

            plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Year2008Regex)) {
                plateReturn = ParseUsScYr2008Plate(plate);
            } else {
                plateReturn = new UsScPlateReturnModel {
                    Valid = false
                };
            }

            return plateReturn;
        }

        public static UsScPlateReturnModel ParseUsScYr2008Plate(string plate)
        {
            Regex regex = new Regex(Year2008Regex);
            Match match = regex.Match(plate);

            UsScPlateReturnModel returnModel = new UsScPlateReturnModel {
                Format = Enums.UsScPlateFormat.yr2008,
                Valid = true
            };

            return returnModel;
        }
    }
}