using System;
using System.Text.RegularExpressions;
using ClunkerBot.Plates.Models;
using ClunkerBot.Plates.Models.ReturnModels;

namespace ClunkerBot.Utilities.PlateUtilities
{
    public class GgPlate
    {
        private static string Standard1908Regex = @"^(([0-9]{1,5}))$";

        public static GgPlateReturnModel ParseGgPlate(string plate)
        {
            GgPlateReturnModel plateReturn = null;

            plate = plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Standard1908Regex))
            {
                plateReturn = ParseStandard1908Plate(plate);
            }
            else
            {
                plateReturn = new GgPlateReturnModel {
                    Valid = false
                };
            }

            plateReturn.CountryFlag = "🇬🇬";
            plateReturn.CountryCode = "gg";

            return plateReturn;
        }

        private static GgPlateReturnModel ParseStandard1908Plate(string plate)
        {
            string issueString = plate;
            string specialString = "";

            if(plate == "1")
            {
                specialString = "Bailiff of Guernsey";
            }

            GgPlateReturnModel returnModel = new GgPlateReturnModel
            {
                Format = Enums.GgPlateFormatEnum.Standard1908,
                Issue = issueString,
                Special = specialString,
                Valid = true
            };

            return returnModel;
        }
    }
}