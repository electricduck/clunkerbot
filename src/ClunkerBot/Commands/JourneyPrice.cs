using System;
using ClunkerBot.Data;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    class JourneyPrice : CommandsBase
    {
        private static string currencySymbol = "¤";
        private static double imperialGallonsLitresRate = 4.54609188;
        private static double imperialGallonsUsGallonsRate = 1.200954;
        private static double kilometerMileRate = 0.6213712;
        private static double usGallonsLitresRate = 3.7854;
        private static bool useUsGallons = false;

        public static string Calculate(string distance, string mpg, string fuelPrice)
        {
            try {
                ResetGlobalValues();
                DetectUsGallonUsage(distance, mpg, fuelPrice);

                double parsedFuelPrice = ParseFuelPrice(fuelPrice);
                double parsedMpg = ParseMpg(mpg);
                double parsedDistance = ParseDistance(distance);

                double gallons = parsedDistance / parsedMpg;
                double volume = 0;

                if(fuelPrice.ToLower().Contains("/g")) {
                    if (useUsGallons) {
                        volume = gallons;
                    } else {
                        volume = gallons * imperialGallonsUsGallonsRate;
                    }
                } else {
                    if (useUsGallons) {
                        volume = gallons * usGallonsLitresRate;
                    } else {
                        volume = gallons * imperialGallonsLitresRate;
                    }
                }

                double penceTotalCost = volume * (parsedFuelPrice * 100);
                double poundsTotalCost = Math.Round((penceTotalCost / 100), 2, MidpointRounding.ToEven);

                string result = $"<b>Fuel Cost:</b> {currencySymbol}{String.Format("{0:f2}", poundsTotalCost)}";

                return BuildOutput(result, "Calculate Journey Price", "🛣️");
            } catch (Exception e) {
                return BuildErrorOutput(e);
            }
        }
    

        private static void DetectUsGallonUsage(string distance, string mpg, string fuelPrice) {
            if(fuelPrice.ToLower().Contains("/g")) {
                useUsGallons = true;
            }

            if(fuelPrice.ToLower().Contains("$")) {
                useUsGallons = true;
            }

            if(mpg.ToLower().Contains("usmpg")) {
                useUsGallons = true;
            }

            if(mpg.ToLower().Contains("/l")) {
                useUsGallons = false;
            }

            if(mpg.ToLower().Contains("impmpg")) {
                useUsGallons = false;
            }

            if(mpg.ToLower().Contains("ukmpg")) {
                useUsGallons = false;
            }
        }

        private static double ParseDistance(string distance) {
            double parsedDistance = 0;
            bool useKm = false;

            if (distance.ToLower().Contains("km")) {
                useKm = true;
            }
            
            distance = distance
                .Replace("mi","")
                .Replace("km", "");

            parsedDistance = Convert.ToDouble(distance);

            if(useKm) {
                parsedDistance = parsedDistance * kilometerMileRate;
            }

            return parsedDistance;
        }

        private static double ParseFuelPrice(string fuelPrice) {
            double parsedFuelPrice = 0;

            if(!System.Char.IsDigit(fuelPrice[0])) {
                currencySymbol = fuelPrice[0].ToString();
            }
            
            if(!System.Char.IsDigit(fuelPrice[0])) {
                fuelPrice = fuelPrice.Replace(fuelPrice[0].ToString(), "");
            }

            fuelPrice = fuelPrice.ToLower().Replace("/g", "").Replace("/l", "");

            parsedFuelPrice = Convert.ToDouble(fuelPrice);

            if(useUsGallons) {
                parsedFuelPrice = parsedFuelPrice * imperialGallonsUsGallonsRate;
            }

            return parsedFuelPrice;
        }

        private static double ParseMpg(string mpg) {
            double parsedMpg = 0;
            
            mpg = mpg.ToLower()
                .Replace("impmpg", "")
                .Replace("usmpg", "")
                .Replace("ukmpg", "")
                .Replace("mpg", "");

            parsedMpg = Convert.ToDouble(mpg);

            if(useUsGallons) {
                parsedMpg = parsedMpg * imperialGallonsUsGallonsRate;
            }

            return parsedMpg;
        }

        private static void ResetGlobalValues() {
            currencySymbol = "¤";
            useUsGallons = false;
        }
    }
}
