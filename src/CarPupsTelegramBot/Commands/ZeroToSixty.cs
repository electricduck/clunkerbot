using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels;
using CarPupsTelegramBot.Models.ReturnModels.MessageReturnModels;

namespace CarPupsTelegramBot.Commands
{
    class ZeroToSixty
    {
        public static string Calculate(string horsepower, string weight, string driveType, string transmission)
        {
            try {
                string output;

                double zeroToSixty = Calculate(Convert.ToDouble(horsepower), Convert.ToDouble(weight), driveType.ToLower(), transmission.ToLower().Substring(0, 3));

                output = $"{zeroToSixty}";

                return output;
            } catch {
                return "oops";
            }
        }

        private static double Calculate(double horsepower, double weight, string driveType, string transmission)
        {
            // Ported from https://www.carspecs.us/calculator/0-60

            double r = 0.875;

            double u = driveType == "rws" ? 0.9 : driveType == "awd" ? 0.85 : 1;
            double f = transmission == "aut" ? 1.1 : transmission == "dct" ? 0.925 : 1;

            double powerToWeightRatio = Math.Round(horsepower / weight * 1e3) / 1e3;
            double zeroToSixty = Math.Round(Math.Pow(weight / horsepower * u * f * r, 0.75) * 1e3) / 1e3;

            return zeroToSixty;
        } 
    }
}