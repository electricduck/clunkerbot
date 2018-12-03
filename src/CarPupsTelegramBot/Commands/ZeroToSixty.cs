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

        public static string Calculate(string power, string weight, string driveType, string transmission, string finalResult = "0-60mph")
        {
            try {
                string output;

                if(finalResult != "0-60mph" && finalResult != "0-100kph") {
                    throw new ArgumentException();
                }

                Match parsedPower = PowerRegex.Match(power);
                Match parsedWeight = WeightRegex.Match(weight);

                double powerValue = Convert.ToDouble(parsedPower.Groups[2].Value);
                string powerUnit = parsedPower.Groups[3].Value;
                double weightValue = Convert.ToDouble(parsedWeight.Groups[2].Value);
                string weightUnit = parsedWeight.Groups[3].Value;

                double zeroToSixty = Calculate(powerValue, weightValue, driveType.ToLower(), transmission.ToLower().Substring(0, 3), finalResult, powerUnit, weightUnit);

                output = $"{zeroToSixty}";

                return output;
            } catch {
                return "oops";
            }
        }

        private static double Calculate(double power, double weight, string driveType, string transmission, string finalResult, string powerUnit = "hp", string weightUnit = "lbs")
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

            double r = finalResult == "0-100kph" ? 0.9 : 0.875;

            double u = driveType == "rws" ? 0.9 : driveType == "awd" ? 0.85 : 1;
            double f = transmission == "aut" ? 1.1 : transmission == "dct" ? 0.925 : 1;

            double powerToWeightRatio = Math.Round(power / weight * 1e3) / 1e3;
            double zeroToSixty = Math.Round(Math.Pow(weight / power * u * f * r, 0.75) * 1e3) / 1e3;

            return zeroToSixty;
        } 
    }
}