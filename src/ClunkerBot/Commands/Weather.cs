using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ClunkerBot.Models;
using ClunkerBot.Services;
using ClunkerBot.Utilities;

namespace ClunkerBot.Commands
{
    public class Weather : CommandsBase
    {
        public static string Get(string location, bool brief = false)
        {
            OwnApiService _ownApiService = new OwnApiService();

            string outputEmoji = "☁️";
            string outputHeader = "Weather";

            try
            {
                string query = "q=" + location;

                string jsonResult = _ownApiService.QueryApiAsync("weather", query).Result;
                JObject parsedJson = JObject.Parse(jsonResult);
                
                string code = (string)parsedJson["cod"];

                if(code == "404") {
                    return BuildOutput("<i>Location not found.</i>", outputHeader, outputEmoji, location);
                }

                string weatherType = (string)parsedJson["weather"][0]["main"];
                string weatherTypeIcon = (string)parsedJson["weather"][0]["icon"];
                string weatherDescription = TextInfo_enUS.ToTitleCase((string)parsedJson["weather"][0]["description"]);
                string locationName = (string)parsedJson["name"];
                string locationCountry = (string)parsedJson["sys"]["country"];

                string temperatureCelcius = ConvertKelvinToSomethingLessSilly((double)parsedJson["main"]["temp"], Enums.Temperatures.Celcius).ToString() + "°C";
                string temperatureFahrenheit = ConvertKelvinToSomethingLessSilly((double)parsedJson["main"]["temp"], Enums.Temperatures.Fahrenheit).ToString() + "°F";
                string temperatureMaxCelcius = ConvertKelvinToSomethingLessSilly((double)parsedJson["main"]["temp_max"], Enums.Temperatures.Celcius).ToString() + "°C";
                string temperatureMaxFahrenheit = ConvertKelvinToSomethingLessSilly((double)parsedJson["main"]["temp_max"], Enums.Temperatures.Fahrenheit).ToString() + "°F";
                string temperatureMinCelcius = ConvertKelvinToSomethingLessSilly((double)parsedJson["main"]["temp_min"], Enums.Temperatures.Celcius).ToString() + "°C";
                string temperatureMinFahrenheit = ConvertKelvinToSomethingLessSilly((double)parsedJson["main"]["temp_min"], Enums.Temperatures.Fahrenheit).ToString() + "°F";

                string pressure = (string)parsedJson["main"]["pressure"] + "hPa";
                string pressure_inHg = UnitConversionUtlities.hPA_inHg((double)parsedJson["main"]["pressure"], 1) + "inHg";
                string humidity = (string)parsedJson["main"]["humidity"] + "%";
                string cloudCover = (string)parsedJson["clouds"]["all"] + "%";
                string visibility = ParseVisibility((double)parsedJson["visibility"]);
                
                string windSpeed = (string)parsedJson["wind"]["speed"] + "m/s";
                string windDirection = ConvertHeadingToCompass((int)parsedJson["wind"]["deg"]);

                long sunrise = (long)parsedJson["sys"]["sunrise"];
                long sunset = (long)parsedJson["sys"]["sunset"];

                string parsedSunrise = ConvertUnixTimestampToDateTime(sunrise).ToString("h:mm tt");;
                string parsedSunset = ConvertUnixTimestampToDateTime(sunset).ToString("h:mm tt");;

                string fullLocation = $"{locationName}, {locationCountry}";
                string icon = GetWeatherIcon(weatherTypeIcon);
                string weather = $@"<b>{weatherType}</b>
<i>{weatherDescription}</i>";

                if(weatherType.ToLower() == weatherDescription.ToLower()) {
                    weather = $"<b>{weatherType}</b>";
                }

                string result = "";

                if(brief) {
                    result = $@"{icon} <b>{weatherDescription}</b>
🌡️ {temperatureCelcius} / {temperatureFahrenheit} | 🌬️ {windSpeed} ({windDirection}) | 💦 {humidity}";
                } else {
                    result = $@"{icon} {weather}

<h2>🌡️ Temperature</h2>
<h3>Current:</h3> {temperatureCelcius} / {temperatureFahrenheit}
<h3>High:</h3> {temperatureMaxCelcius} / {temperatureMaxFahrenheit}
<h3>Low:</h3> {temperatureMinCelcius} / {temperatureMinFahrenheit}

<h2>☁️ Atmosphere</h2>
<h3>Pressure:</h3> {pressure} / {pressure_inHg}
<h3>Humidity:</h3> {humidity}
<h3>Wind:</h3> {windSpeed} ({windDirection})
<h3>Clouds:</h3> {cloudCover}
<h3>Visibility:</h3> {visibility}

<h2>☀️ Day Cycle</h2>
<h3>Sunrise:</h3> {parsedSunrise}
<h3>Sunset:</h3> {parsedSunset}";
                }

                return BuildOutput(result, outputHeader, outputEmoji, fullLocation);
            } catch (Exception e) {
                return BuildErrorOutput(e);
            }
        }

        public static string ConvertHeadingToCompass(int heading){
            var directions = new string[] {
                "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N"
            };
            var index = (heading + 23) / 45;
            
            return directions[index];
        }

        private static double ConvertKelvinToSomethingLessSilly(double kelvin, Enums.Temperatures outputTemperature)
        {
            if(outputTemperature == Enums.Temperatures.Celcius) {
                return Math.Round(kelvin - 273.15, 1);
            } else if(outputTemperature == Enums.Temperatures.Fahrenheit) {
                return Math.Round((kelvin * 1.8) - 459.67, 1);
            } else {
                return Math.Round(kelvin, 1);
            }
        }
        
        private static DateTime ConvertUnixTimestampToDateTime(long unixTimestamp)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(unixTimestamp));
        }

        private static string ParseVisibility(double visibility)
        {
            if(visibility >= 1000) {
                return "∞";
            } else {
                return visibility + "M";
            }
        }

        private static string GetWeatherIcon(string icon)
        {
            if(WeatherIcons.ContainsKey(icon)) {
                string emoji;

                WeatherIcons.TryGetValue(icon, out emoji);

                return emoji;
            } else {
                return "☁️";
            }
        }

        private static Dictionary<string, string> WeatherIcons = new Dictionary<string, string>()
        {
            {"01d", "☀️"},  // Day: Sunny
            {"01n", "🌙"},  // Night: Clear
            {"02d", "⛅"},  // Day: Partially Cloudy
            {"02n", "☁️"},  // Night: Partially Cloudy
            {"03d", "🌥️"},  // Day: Cloudy
            {"03n", "☁️"},  // Night: Cloudy
            {"04d", "☁️"},  // Day: Cloudy (alt)
            {"04n", "☁️"},  // Night: Cloudy (alt)
            {"09d", "🌦️"},  // Day: Showers
            {"09n", "🌧️"},  // Night: Showers
            {"10d", "🌧️"},  // Day: Rain
            {"10n", "🌧️"},  // Night: Rain
            {"13d", "🌩️"},  // Day: Thunderstorm
            {"13n", "🌩️"},  // Night: Thunderstorm
            {"50d", "🌫️"},  // Day: Fog
            {"50n", "🌫️"},  // Night: Fog
            {"900", "🌪️"},  // Extreme: Tornado
            {"901", "️️⛈️"},  // Extreme: Storm & Showers
            {"902", "🌀"},  // Extreme: Hurricane
            {"903", "❄️"},  // Extreme: Snow
            {"904", "🥵"},  // Extreme: Very Hot
            {"905", "🥶"},  // Extreme: Heavy Wind
            {"906", "🌨️"}  // Extreme: Hail
        };
    }
}