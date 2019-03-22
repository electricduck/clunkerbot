using System;

namespace ClunkerBot.Data
{
    class AppVersion
    {
        public static readonly int Major = 19;
        public static readonly int Minor = 11;
        public static readonly int Patch = 2;
        public static readonly string Release = "Fabia";

        public static readonly string FullVersion = $"{Major}.{Minor}.{Patch}";
    }
}
