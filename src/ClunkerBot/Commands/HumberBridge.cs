using System;
using HtmlAgilityPack;
using ClunkerBot.Models;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    class HumberBridge : CommandsBase
    {
        public static string GetConditionsOn()
        {
            string outputEmoji = "🌉";
            string outputHeader = "Humber Bridge Status";

            try
            {
                string humberBridgeBaseUrl = "https://www.humberbridge.co.uk/";

                var url = humberBridgeBaseUrl;
                var web = new HtmlWeb();
                var document = web.Load(url);
                var documentNode = document.DocumentNode;

                var bridgeStatus = documentNode.SelectNodes("//p[contains(@class, 'bridgestatus')]")[0].InnerText;
                var restrictions = documentNode.SelectNodes("//p[contains(@class, 'restrictions')]")[0].InnerText;
                var weatherDays = documentNode.SelectNodes("//div[contains(@id, 'weather')]//div//h1");
                var weatherIcons = documentNode.SelectNodes("//div[contains(@id, 'weather')]//div//img");
                var windDirection = documentNode.SelectNodes("//p[contains(@class, 'winddirection')]")[0].InnerText;
                var windSpeed = documentNode.SelectNodes("//div[contains(@class, 'windspeed')]//h1")[0].InnerText.Replace("\n", "").Replace("\t", "");
                var windSpeedUnit = documentNode.SelectNodes("//div[contains(@class, 'windspeed')]//h1")[1].InnerText;

                var day0Weather = GetWeather(0, weatherIcons, weatherDays);
                var day1Weather = GetWeather(1, weatherIcons, weatherDays);
                var day2Weather = GetWeather(2, weatherIcons, weatherDays);
                var day3Weather = GetWeather(3, weatherIcons, weatherDays);

                var bridgeStatusIcon = "✔️";

                if(bridgeStatus != "Open")
                {
                    bridgeStatusIcon = "❌";
                }

                string result = $@"<b>{bridgeStatusIcon} {bridgeStatus}</b>
{restrictions}

<b>☁️ Conditions</b>
<i>Wind Speed:</i> {windSpeed}{windSpeedUnit}
<i>Wind Direction:</i>{windDirection}

<b>☀️ Weather</b>
{day0Weather}
{day1Weather}
{day2Weather}
{day3Weather}";

                return BuildOutput(result, outputHeader, outputEmoji);
            }
            catch(Exception e)
            {
                return BuildErrorOutput(e);
            }
        }

        private static string ConvertShortDayToLongDay(string shortDay)
        {
            switch(shortDay.ToLower())
            {
                case "mon":
                    return "Monday";
                case "tue":
                    return "Tuesday";
                case "wed":
                    return "Wednesday";
                case "thu":
                    return "Thursday";
                case "fri":
                    return "Friday";
                case "sat":
                    return "Saturday";
                case "sun":
                    return "Sunday";
            }

            return "Today";
        }

        private static string GetWeather(int day, HtmlNodeCollection iconsNodes, HtmlNodeCollection daysNodes)
        {
            var dayNode = ConvertShortDayToLongDay(daysNodes[day].InnerText);
            var iconNode = iconsNodes[day].Attributes["src"].Value;

            var iconCode = GetWeatherIconFromImgSrc(iconNode);

            var dayText = dayNode;
            var weatherIcon = WeatherUtilities.GetWeatherIcon(iconCode);
            var weatherText = WeatherUtilities.GetWeatherText(iconCode);

            return $@"<i>{dayText}:</i> {weatherIcon} {weatherText}";
        }

        private static string GetWeatherIconFromImgSrc(string imgSrcValue)
        {
            return imgSrcValue
                .Replace("https://www.humberbridge.co.uk/wp-content/plugins/BridgeConditions/images/icons/", "")
                .Replace(".png", "");
        }
    }
}