using System;
using Telegram.Bot.Args;

namespace ClunkerBot.Utilities
{
    class ConsoleOutputUtilities
    {
        public static void MessageInConsoleMessage(MessageEventArgs telegramMessageEvent)
        {
            Console.WriteLine($"➡️ {telegramMessageEvent.Message.Chat.Id} ({telegramMessageEvent.Message.Chat.Username}) | {telegramMessageEvent.Message.From.Id} ({telegramMessageEvent.Message.From.Username}) | {telegramMessageEvent.Message.Text}");
        }

        public static void MessageOutConsoleMessage(string message, MessageEventArgs telegramMessageEvent)
        {
            Console.WriteLine($"⬅️ {telegramMessageEvent.Message.Chat.Id} ({telegramMessageEvent.Message.Chat.Username}) | {telegramMessageEvent.Message.From.Id} ({telegramMessageEvent.Message.From.Username}) | {message}");
        }

        public static void DatabaseSaveConsoleMessage(int count) {
            Console.WriteLine($"🛢️ {count} records updated");
        }

        public static void SeparatorConsoleMessage() {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"========================================");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SeparatorMinorConsoleMessage() {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"----------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void InfoConsoleMessage(string message) {
            Console.WriteLine($"ℹ️ {message}");
        }

        public static void DoingConsoleMessage(string message) {
            Console.WriteLine($"⚙️ {message}...");
        }

        public static void OkayConsoleMessage(string message) {
            Console.WriteLine($"✔️ {message}");
        }

        public static void ErrorConsoleMessage(string message) {
            Console.WriteLine("⚠️ " + message);
        }
    }
}