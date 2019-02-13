using System;

namespace ClunkerBot.Data
{
    public class AppSettings
    {
        public static string ApiKeys_OpenWeatherMap { get; set; }
        public static string ApiKeys_Telegram { get; set; }
        public static bool Config_Awoo_Repeat { get; set; }
        public static string Config_Awoo_Word { get; set; }
        public static string Config_BotUsername { get; set; }
        public static string Endpoints_PlateWtf { get; set; }
    }
}
