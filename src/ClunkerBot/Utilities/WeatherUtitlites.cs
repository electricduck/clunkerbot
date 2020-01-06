using System;
using System.Collections.Generic;

namespace ClunkerBot.Utilities
{
    class WeatherUtilities
    {
        public static string GetWeatherIcon(string icon)
        {
            if(WeatherIcons.ContainsKey(icon)) {
                string emoji;

                WeatherIcons.TryGetValue(icon, out emoji);

                return emoji;
            } else {
                return "☁️";
            }
        }

        public static string GetWeatherText(string icon)
        {
            if(WeatherText.ContainsKey(icon)) {
                string emoji;

                WeatherText.TryGetValue(icon, out emoji);

                return emoji;
            } else {
                return "Unknown Weather";
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

        private static Dictionary<string, string> WeatherText = new Dictionary<string, string>()
        {
            {"01d", "Sunny"},
            {"01n", "Clear"},
            {"02d", "Partially Cloud"},
            {"02n", "Partially Cloudy"},
            {"03d", "Cloudy"},
            {"03n", "Cloudy"},
            {"04d", "Cloudy"},
            {"04n", "Cloudy"},
            {"09d", "Showers"},
            {"09n", "Showers"},
            {"10d", "Rain"},
            {"10n", "Rain"},
            {"13d", "Thunderstorm"},
            {"13n", "Thunderstorm"},
            {"50d", "Fog"},
            {"50n", "Fog"},
            {"900", "Tornado"},
            {"901", "️️Storm & Showers"},
            {"902", "Hurricane"},
            {"903", "Snow"},
            {"904", "Very Hot"},
            {"905", "Heavy Wind"},
            {"906", "Hail"}
        };
    }
}