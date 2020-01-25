using System;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    class CarbonNeutral : CommandsBase
    {
        public static string Calculate(string emissions, string mileage)
        {
            string outputEmoji = "🌳";
            string outputHeader = "Carbon Neutral";

            try {
                var normalizedEmissions = UnitConversionUtilities.NormalizeEmissions(emissions);
                var normalizedMileage = UnitConversionUtilities.NormalizeDistance(mileage);
            
                if(
                    normalizedEmissions.Calculated == false ||
                    normalizedMileage.Calculated == false
                ) {
                    return BuildSoftErrorOutput("Unknown unit. See <code>/help co2</code>.");
                }

                double treeCo2AbsorbtionPerYearInGrams = 21770;

                var treesPerYear = Math.Round((normalizedEmissions.Value * normalizedMileage.Value) / treeCo2AbsorbtionPerYearInGrams);
                var treesPerMonth = Math.Round(treesPerYear / 12);

                string result = $@"<b>Trees (per year):</b> {treesPerYear}
<b>Trees (per month):</b> {treesPerMonth}

<i>This is how many trees you'd need to plant for your driving to be carbon neutral, assuming the average tree absorbs 21.77kg of CO2 per year.</i>";

                return BuildOutput(result, outputHeader, outputEmoji);
            } catch(Exception e) {
                return BuildErrorOutput(e);
            }
        }
    }
}