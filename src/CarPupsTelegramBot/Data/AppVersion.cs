using System;

namespace CarPupsTelegramBot.Data
{
    class AppVersion
    {
        public static readonly int Major = 18;
        public static readonly int Minor = 8;
        public static readonly int Patch = 1;

        public static readonly string FullVersion = $"{Major}.{Minor}.{Patch}";
    }
}
