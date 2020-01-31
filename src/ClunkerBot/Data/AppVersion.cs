using System;

namespace ClunkerBot.Data
{
    class AppVersion
    {
        public static readonly int Major = 20;
        public static readonly int Minor = 2;
        public static readonly int Patch = 5;
        public static readonly string Release = "Hilux";

        public static readonly string FullVersion = $"{Major}.{Minor}.{Patch}";
    }
}
