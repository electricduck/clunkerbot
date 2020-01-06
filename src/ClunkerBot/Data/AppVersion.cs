using System;

namespace ClunkerBot.Data
{
    class AppVersion
    {
        public static readonly int Major = 20;
        public static readonly int Minor = 1;
        public static readonly int Patch = 1;
        public static readonly string Release = "Golf";

        public static readonly string FullVersion = $"{Major}.{Minor}.{Patch}";
    }
}
