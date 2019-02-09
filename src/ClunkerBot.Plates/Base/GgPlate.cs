using System;
using System.Text.RegularExpressions;
using ClunkerBot.Plates.Models.ReturnModels;

namespace ClunkerBot.Utilities.PlateUtilities
{
    public class GgPlate
    {
        private static string AnyRegex = @"^([0-9]{1,5})$";

        public static GgPlateReturnModel ParseGgPlate(string plate)
        {
            GgPlateReturnModel plateReturn;

            if(Regex.IsMatch(plate, AnyRegex)) {
                plateReturn = new GgPlateReturnModel {
                    Issue = Convert.ToInt32(plate),
                    Valid = true
                };
            } else {
                plateReturn = new GgPlateReturnModel {
                    Valid = false
                };
            }

            plateReturn.CountryFlag = "🇬🇬";
            plateReturn.CountryCode = "gg";

            return plateReturn;
        }
    }
}