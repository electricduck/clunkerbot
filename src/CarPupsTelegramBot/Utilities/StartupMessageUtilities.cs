using System;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot.Utilities
{
    class StartupMessageUtlities
    {
        public static string Figlet = @"  ____ _             _             ____        _   
 / ___| |_   _ _ __ | | _____ _ __| __ )  ___ | |_ 
| |   | | | | | '_ \| |/ / _ | '__|  _ \ / _ \| __|
| |___| | |_| | | | |   |  __| |  | |_) | (_) | |_ 
 \____|_|\__,_|_| |_|_|\_\___|_|  |____/ \___/ \__|
===================================================";

        public static void GenerateStartupMessage()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(Figlet);

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine($"ClunkerBot. Version {AppVersion.Major}.{AppVersion.Minor}.{AppVersion.Patch}.");
            Console.WriteLine($"Written by ElectricDuck. Licensed under MIT.");

            Console.ForegroundColor = ConsoleColor.White;
        
            ConsoleOutputUtilities.SeparatorConsoleMessage();
        }
    }
}