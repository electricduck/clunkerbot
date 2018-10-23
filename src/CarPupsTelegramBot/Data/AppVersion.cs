using System;

namespace CarPupsTelegramBot.Data
{
    class AppVersion
    {
        public static readonly int Major = 0;
        public static readonly int Minor = 2;
        public static readonly int Patch = 0;

        public static readonly string FullVersion = $"{Major}.{Minor}.{Patch}";
    }
}