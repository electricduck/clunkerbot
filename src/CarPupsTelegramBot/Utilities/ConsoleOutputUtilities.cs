using System;
using Telegram.Bot.Args;

namespace CarPupsTelegramBot.Utilities
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
    }
}