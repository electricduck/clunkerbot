using System;
using Telegram.Bot.Args;

namespace ClunkerBot.Utilities
{
    class ConsoleOutputUtilities
    {
        public static void ResetConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void MessageInConsoleMessage(MessageEventArgs telegramMessageEvent)
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff zzz");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🔻 {currentDateTime} | {telegramMessageEvent.Message.Chat.Id} ({telegramMessageEvent.Message.Chat.Username}) | {telegramMessageEvent.Message.From.Id} ({telegramMessageEvent.Message.From.Username})");
            ResetConsoleColor();
            Console.WriteLine($"{telegramMessageEvent.Message.Text}");
        }

        public static void MessageOutConsoleMessage(string message, MessageEventArgs telegramMessageEvent)
        {
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff zzz");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🔺 {currentDateTime} | {telegramMessageEvent.Message.Chat.Id} ({telegramMessageEvent.Message.Chat.Username}) | {telegramMessageEvent.Message.From.Id} ({telegramMessageEvent.Message.From.Username})");
            ResetConsoleColor();
            Console.WriteLine($"{message}");
        }

        public static void DatabaseSaveConsoleMessage(int count) {
            Console.WriteLine($"🛢️ {count} records updated");
        }

        public static void SeparatorConsoleMessage() {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"========================================");
            ResetConsoleColor();
        }

        public static void SeparatorMinorConsoleMessage() {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"----------------------------------------");
            ResetConsoleColor();
        }

        public static void InfoConsoleMessage(string message) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"ℹ️ {message}");
            ResetConsoleColor();
        }

        public static void DoingConsoleMessage(string message) {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"⚙️ {message}...");
            ResetConsoleColor();
        }

        public static void OkayConsoleMessage(string message) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✔️ {message}");
            ResetConsoleColor();
        }

        public static void ErrorConsoleMessage(string message, string identifier = "") {
            Console.ForegroundColor = ConsoleColor.Red;

            if(String.IsNullOrEmpty(identifier)) {
                Console.WriteLine("❌ Error: " + message);
            } else {
                Console.WriteLine("❌ Error: " + identifier + Environment.NewLine + message);
            }

            ResetConsoleColor();
        }

        public static void WarnConsoleMessage(string message) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠️ " + message);
            ResetConsoleColor();
        }
    }
}