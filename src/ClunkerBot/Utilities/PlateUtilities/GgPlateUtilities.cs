using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClunkerBot.Models;
using ClunkerBot.Models.ReturnModels.PlateReturnModels;

namespace ClunkerBot.Utilities.PlateUtilities
{
    public class GgPlateUtilities
    {
        private static string AllRegex = @"^([0-9]{1,5})$";

        public static GgPlateReturnModel ParseGgPlate(string plate)
        {
            GgPlateReturnModel ggPlateReturn;

            if(Regex.IsMatch(plate, AllRegex)) {
                ggPlateReturn = new GgPlateReturnModel {
                    Issue = Convert.ToInt32(plate),
                    Valid = true
                };
            } else {
                ggPlateReturn = new GgPlateReturnModel {
                    Valid = false
                };
            }

            return ggPlateReturn;
        }
    }
}