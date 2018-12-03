using System;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot.Utilities
{
    class StartupMessageUtlities
    {
        public static string Figlet = @"  ____ ____ _____ ____  
 / ___|  _ |_   _| __ ) 
| |   | |_) || | |  _ \ 
| |___|  __/ | | | |_) |
 \____|_|    |_| |____/ 
========================";

        public static void GenerateStartupMessage()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(Figlet);

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine($"CarPupsTelegramBot. Version {AppVersion.Major}.{AppVersion.Minor}.{AppVersion.Patch}.");
            Console.WriteLine($"Written by ElectricDuck. Licensed under MIT.");

            Console.ForegroundColor = ConsoleColor.White;
        
            ConsoleOutputUtilities.SeparatorConsoleMessage();
        }
    }
}