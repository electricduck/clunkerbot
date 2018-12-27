using System;
using Telegram.Bot.Args;
using ClunkerBot.Api;
using ClunkerBot.Data;

namespace ClunkerBot.Commands
{
    class Mileage : CommandsBase
    {
        public static string Guess(string dateRegistered, int lastMotMileage, string lastMotDate, string dateToCalculateTo = "")
        {
            try {
                DateTime currentDate = DateTime.Now;

                if(!String.IsNullOrEmpty(dateToCalculateTo)) {
                    currentDate = DateTime.Parse(dateToCalculateTo);
                } else if(dateToCalculateTo == "today" || dateToCalculateTo == "now") {
                    currentDate = DateTime.Now;
                }

                double daysSinceRegistration = (currentDate - DateTime.Parse(dateRegistered)).TotalDays;
                double daysSinceLastMot = (DateTime.Parse(lastMotDate) - DateTime.Parse(dateRegistered)).TotalDays;

                double approxMilesPerDay = lastMotMileage / daysSinceLastMot;
                double approxCurrentMileage = approxMilesPerDay * daysSinceRegistration;

                double calculatedMileage = Math.Round(approxCurrentMileage);
                string calculatedMileageFormatted = calculatedMileage.ToString("N0");
                string unit = "Miles";

                string result = $@"<h2>Approx. Mileage:</h2> {calculatedMileageFormatted.ToString()} {unit}
<h2>For Date:</h2> {currentDate.ToString("dd-MMM-yyyy")}";

                return BuildOutput(result, "Guess Mileage", "🚘");
            } catch (Exception e) {
                return BuildErrorOutput(e);
            }
        }
    }
}
