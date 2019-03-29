using System;

namespace ClunkerBot.Data
{
    class AppVersion
    {
        public static readonly int Major = 19;
        public static readonly int Minor = 13;
        public static readonly int Patch = 1;
        public static readonly string Release = "Calibra";

        public static readonly string FullVersion = $"{Major}.{Minor}.{Patch}";
    }
}
