using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels;
using CarPupsTelegramBot.Models.ReturnModels.MessageReturnModels;

namespace CarPupsTelegramBot.Commands
{
    class ZeroToSixty
    {
        private static Regex PowerRegex = new Regex(@"^(([0-9]{1,})((hp|ps|kw))?)$");
        private static Regex WeightRegex = new Regex(@"^(([0-9]{1,})((lbs|kg))?)$");
        private static Regex VolumeRegex = new Regex(@"^(([0-9]{1,})((l|gal))?)$");
        private static double AverageHumanWeight = 85;
        private static double AveragePetrolWeight = 0.77;
        private static double AverageDieselWeight = 0.875;

        public static string Calculate(string power, string weight, string driveType, string transmission, string passengers = "0", string fuelVolume = "0L", string fuelType = "petrol")
        {
            try {
                string output;

                int passengersInt = Convert.ToInt32(passengers);

                Match parsedPower = PowerRegex.Match(power.ToLower());
                Match parsedWeight = WeightRegex.Match(weight.ToLower());
                Match parsedFuelVolume = VolumeRegex.Match(fuelVolume.ToLower());

                double powerValue = Convert.ToDouble(parsedPower.Groups[2].Value);
                string powerUnit = parsedPower.Groups[3].Value;
                double weightValue = Convert.ToDouble(parsedWeight.Groups[2].Value);
                string weightUnit = parsedWeight.Groups[3].Value;
                double fuelVolumeValue = Convert.ToDouble(parsedFuelVolume.Groups[2].Value);
                string fuelVolumeUnit = parsedFuelVolume.Groups[3].Value;

                fuelType = fuelType.ToLower();

                if(passengersInt != 0) {
                    weightValue += (passengersInt*AverageHumanWeight);
                }

                if(fuelVolumeValue != 0) {
                    if(fuelVolumeUnit == "l" || String.IsNullOrEmpty(fuelVolumeUnit)) {
                        weightValue += fuelType == "petrol" ? (fuelVolumeValue*AveragePetrolWeight) : fuelType == "diesel" ? fuelVolumeValue*AverageDieselWeight : 0;
                    } else if(fuelVolumeUnit == "gal") {
                        fuelVolumeValue = fuelVolumeValue*0.22;
                        weightValue += fuelType == "petrol" ? (fuelVolumeValue*AveragePetrolWeight) : fuelType == "diesel" ? fuelVolumeValue*AverageDieselWeight : 0;
                    }
                }

                ZeroToSixtyCalculationReturnModel zeroToSixtyResult = Calculate(powerValue, weightValue, driveType.ToLower(), transmission.ToLower().Substring(0, 3), powerUnit.ToLower(), weightUnit.ToLower());

                output = $@"⏱️ <i>Calculate 0-60</i>
—
<b>0-60mph:</b> {zeroToSixtyResult.ZeroToSixtyMphAcceleration}s
<b>0-100kph:</b> {zeroToSixtyResult.ZeroToOneHundredKphAcceleration}s

<i>This calculation includes {passengersInt} passengers, and {fuelVolumeValue}{fuelVolumeUnit} of {fuelType} on board.</i>";

                return output;
            } catch {
                return HelpData.GetHelp("calculate0to60", true);
            }
        }

        private static ZeroToSixtyCalculationReturnModel Calculate(double power, double weight, string driveType, string transmission, string powerUnit = "hp", string weightUnit = "lbs")
        {
            // Ported from https://www.carspecs.us/calculator/0-60

            if(powerUnit == "ps") {
                power *= 0.9863;
            } else if(powerUnit == "kw") {
                power *= 1.341;
            }

            if(weightUnit == "kg") {
                weight *= 2.2046;
            }

            double u = driveType == "rwd" ? 0.9 : driveType == "awd" ? 0.85 : 1;
            double f = transmission == "aut" ? 1.1 : transmission == "dct" ? 0.925 : 1;

            double powerToWeightRatio = Math.Round(power / weight * 1e3) / 1e3;

            double zeroToSixty = Math.Round(Math.Pow(weight / power * u * f * 0.875, 0.75) * 1e3) / 1e3;
            double zeroToOneHundred = Math.Round(Math.Pow(weight / power * u * f * 0.9, 0.75) * 1e3) / 1e3;

            ZeroToSixtyCalculationReturnModel zeroToSixtyCalculationReturn = new ZeroToSixtyCalculationReturnModel {
                ZeroToSixtyMphAcceleration = zeroToSixty,
                ZeroToOneHundredKphAcceleration = zeroToOneHundred
            };

            return zeroToSixtyCalculationReturn;
        } 
    }
}