using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClunkerBot.Plates.Models;
using ClunkerBot.Plates.Models.ReturnModels;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Spain#Colour_plates
namespace ClunkerBot.Plates
{
    public class EsPlate
    {
        private static string Standard2000Regex = @"^(([0-9]{4})([B|C|D|F|G|H|J|K|L|M|N|P|R|S|T|V|W|X|Y]{3})([C|E|H|P|R|S|T|V]{0,1}))$";
        private static string Standard1971Regex = @"^(([A-Z]{1,3})-([0-9]{4})-([A-Z]{1,2}))$";
        private static string Standard1900Regex = @"^(([A-Z]{1,3})-([0-9]{1,6}))$";

        public static EsPlateReturnModel ParseEsPlate(string plate)
        {
            EsPlateReturnModel plateReturn = null;

            plate = plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Standard2000Regex))
            {
                plateReturn = ParseStandard2000Plate(plate);
            }
            else
            {
                plateReturn = new EsPlateReturnModel
                {
                    Valid = false
                };
            }

            plateReturn.CountryFlag = "🇪🇸";
            plateReturn.CountryCode = "es";

            return plateReturn;
        }

        private static EsPlateReturnModel ParseStandard2000Plate(string plate)
        {
            Regex regex = new Regex(Standard2000Regex);
            Match match = regex.Match(plate);

            EsPlateReturnModel returnModel = new EsPlateReturnModel
            {
                Format = Enums.EsPlateFormatEnum.Standard2000,
                Valid = true
            };

            return returnModel;
        }
    }
}