using System;

namespace ClunkerBot.Data
{
    class AppVersion
    {
        public static readonly int Major = 19;
        public static readonly int Minor = 16;
        public static readonly int Patch = 3;
        public static readonly string Release = "Escort";

        public static readonly string FullVersion = $"{Major}.{Minor}.{Patch}";
    }
}
