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

        public static string Calculate(string power, string weight, string driveType, string transmission)
        {
            try {
                string output;

                Match parsedPower = PowerRegex.Match(power);
                Match parsedWeight = WeightRegex.Match(weight);

                double powerValue = Convert.ToDouble(parsedPower.Groups[2].Value);
                string powerUnit = parsedPower.Groups[3].Value;
                double weightValue = Convert.ToDouble(parsedWeight.Groups[2].Value);
                string weightUnit = parsedWeight.Groups[3].Value;

                double zeroToSixty = Calculate(powerValue, weightValue, driveType.ToLower(), transmission.ToLower().Substring(0, 3), powerUnit, weightUnit);

                output = $"{zeroToSixty}";

                return output;
            } catch {
                return "oops";
            }
        }

        private static double Calculate(double power, double weight, string driveType, string transmission, string powerUnit = "lbs", string weightUnit = "kg")
        {
            // Ported from https://www.carspecs.us/calculator/0-60

            double r = 0.875;

            double u = driveType == "rws" ? 0.9 : driveType == "awd" ? 0.85 : 1;
            double f = transmission == "aut" ? 1.1 : transmission == "dct" ? 0.925 : 1;

            double powerToWeightRatio = Math.Round(power / weight * 1e3) / 1e3;
            double zeroToSixty = Math.Round(Math.Pow(weight / power * u * f * r, 0.75) * 1e3) / 1e3;

            return zeroToSixty;
        } 
    }
}