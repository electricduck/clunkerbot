using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels;

// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_France

namespace CarPupsTelegramBot.Utilities.PlateUtilities
{
    public class FrPlateUtilities
    {
        private static string Year2009Regex = @"^(([A-Z]{2})-([0-9]{3})-([A-Z]{2}))$";
    
        public static FrPlateReturnModel ParseFrPlate(string plate)
        {
            FrPlateReturnModel plateReturn = null;

            plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Year2009Regex)) {
                plateReturn = ParseFrYr2009Plate(plate);
            } else {
                plateReturn = new FrPlateReturnModel {
                    Valid = false
                };
            }

            return plateReturn;
        }

        private static FrPlateReturnModel ParseFrYr2009Plate(string plate)
        {
            Regex regex = new Regex(Year2009Regex);
            Match match = regex.Match(plate);

            FrPlateReturnModel returnModel = new FrPlateReturnModel {
                Format = Enums.FrPlateFormat.yr2002,
                Valid = true
            };

            int issue = GetPost2009Issue(
                match.Groups[2].Value,
                match.Groups[4].Value,
                Convert.ToInt32(match.Groups[3].Value)
            );

            returnModel.Issue = issue.ToString();

            return returnModel;
        }

        // TODO: Ignore I, O, and U letters
        private static int GetPost2009Issue(string firstLetters, string lastLetters, int number)
        {
            int result = 0;

            string letters = $@"{firstLetters}{lastLetters}";

            for (int i = 0; i < letters.Length; i++)
            {
                result *= 26;
                char letter = letters[i];

                if (letter < 'A') letter = 'A';
                if (letter > 'Z') letter = 'Z';

                result += (int)letter - (int)'A' + 1;
            }

            result = (result-18278)*999+number-999;

            return result;
        }
    }
}