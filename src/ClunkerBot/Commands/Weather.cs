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
        public static string Get(string location)
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
                    return BuildOutput(outputEmoji, outputHeader, location, "<i>Location not found.</i>");
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
                string humidity = (string)parsedJson["main"]["humidity"] + "%";
                string cloudCover = (string)parsedJson["clouds"]["all"] + "%";
                string visibility = ParseVisibility((double)parsedJson["visibility"]);
                
                string windSpeed = (string)parsedJson["wind"]["speed"] + "m/s";
                string windDirection = (string)parsedJson["wind"]["deg"] + "°";

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

                string result = $@"{icon} {weather}

<header>🌡️ Temperature</header>
<subitem>Current:</subitem> {temperatureCelcius} / {temperatureFahrenheit}
<subitem>High:</subitem> {temperatureMaxCelcius} / {temperatureMaxFahrenheit}
<subitem>Low:</subitem> {temperatureMinCelcius} / {temperatureMinFahrenheit}

<header>☁️ Atmosphere</header>
<subitem>Pressure:</subitem> {pressure}
<subitem>Humidity:</subitem> {humidity}
<subitem>Wind:</subitem> {windSpeed} ({windDirection})
<subitem>Clouds:</subitem> {cloudCover}
<subitem>Visibility:</subitem> {visibility}

<header>☀️ Day Cycle</header>
<subitem>Sunrise:</subitem> {parsedSunrise}
<subitem>Sunset:</subitem> {parsedSunset}";

                return BuildOutput(outputEmoji, outputHeader, fullLocation, result);
            } catch (Exception e) {
                return BuildErrorOutput(e);
            }
        }

        private static double ConvertKelvinToSomethingLessSilly(double kelvin, Enums.Temperatures outputTemperature)
        {
            if(outputTemperature == Enums.Temperatures.Celcius) {
                return Math.Round(kelvin - 273.15, 2);
            } else if(outputTemperature == Enums.Temperatures.Fahrenheit) {
                return Math.Round((kelvin * 1.8) - 459.67, 2);
            } else {
                return Math.Round(kelvin, 2);
            }
        }
        
        private static DateTime ConvertUnixTimestampToDateTime(long unixTimestamp)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(unixTimestamp));
        }

        //private static string GetDegreeValue(int degree)
        //{
        //    return ["N","NNE","NE","ENE","E","ESE","SE","SSE","S","SSW","SW","WSW","W","WNW","NW","NNW","N"][Math.round((degreeValue/22.5)+.5 % 16)];
        //}

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