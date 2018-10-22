using System;
using Telegram.Bot.Args;
using CarPupsTelegramBot.Api;
using CarPupsTelegramBot.Data;

namespace CarPupsTelegramBot.Commands
{
    class Mileage
    {
        public static string Guess(string dateRegistered, int lastMotMileage, string lastMotDate, string dateToCalculateTo = "")
        {
            try {
                DateTime currentDate = DateTime.Now;

                if(!String.IsNullOrEmpty(dateToCalculateTo)) {
                    currentDate = DateTime.Parse(dateToCalculateTo);
                }

                double daysSinceRegistration = (currentDate - DateTime.Parse(dateRegistered)).TotalDays;
                double daysSinceLastMot = (DateTime.Parse(lastMotDate) - DateTime.Parse(dateRegistered)).TotalDays;

                double approxMilesPerDay = lastMotMileage / daysSinceLastMot;
                double approxCurrentMileage = approxMilesPerDay * daysSinceRegistration;

                double calculatedMileage = Math.Round(approxCurrentMileage);
                string calculatedMileageFormatted = calculatedMileage.ToString("N0");
                string unit = "Miles";

                string output = $"Approximate mileage for <b>{currentDate.ToString("dd-MMM-yyyy")}</b> is <b>{calculatedMileageFormatted.ToString()} {unit}</b>";

                return output;
            } catch {
                return HelpData.GetHelp("guessmileage", true);
            }
        }
    }
}
