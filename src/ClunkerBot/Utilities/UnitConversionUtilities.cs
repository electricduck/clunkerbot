using System;
using System.Text.RegularExpressions;
using ClunkerBot.Models.ReturnModels;

namespace ClunkerBot.Utilities
{
    class UnitConversionUtilities
    {
         private static string distanceRegex = @"^(([0-9]{1,99})((km)|(mi)){0,1})$";
         private static string emissionsRegex = @"^(([0-9]{1,99})((g\/km)){0,1})$";

        public static NormalizedUnitReturnModel NormalizeDistance(string distanceInput)
        {
            bool outputCalculated = false;
            string outputUnit = "km";
            double outputValue = 0;

            Regex regex = new Regex(distanceRegex);
            Match match = regex.Match(distanceInput);

            if(match.Success) {
                string matchedUnit = match.Groups[3].Value.ToLower();
                double matchedValue = Convert.ToDouble(match.Groups[2].Value);

                switch(matchedUnit)
                {
                    case "": // Assume mi
                    case "mi":
                        outputCalculated = true;
                        outputValue = (matchedValue * 1.609344);
                        break;
                    case "km":
                        outputCalculated = true;
                        outputValue = matchedValue;
                        break;
                }
            }

            NormalizedUnitReturnModel calculatedResult = new NormalizedUnitReturnModel {
                Calculated = outputCalculated,
                Unit = outputUnit,
                Value = outputValue
            };

            return calculatedResult;
        }

        public static NormalizedUnitReturnModel NormalizeEmissions(string emissionsInput)
        {
            bool outputCalculated = false;
            string outputUnit = "g/km";
            double outputValue = 0;

            Regex regex = new Regex(emissionsRegex);
            Match match = regex.Match(emissionsInput);
            
            if(match.Success) {
                string matchedUnit = match.Groups[3].Value.ToLower();
                double matchedValue = Convert.ToDouble(match.Groups[2].Value);

                switch(matchedUnit)
                {
                    case "": // Assume g/km
                    case "g/km":
                        outputCalculated = true;
                        outputValue = matchedValue;
                        break;
                }
            }

            NormalizedUnitReturnModel calculatedResult = new NormalizedUnitReturnModel {
                Calculated = outputCalculated,
                Unit = outputUnit,
                Value = outputValue
            };

            return calculatedResult;
        }

        public static double hPA_inHg(double hpaUnit, int round = 2)
        {
            return Math.Round((hpaUnit * 0.029529983071445), round);
        }
    }
}