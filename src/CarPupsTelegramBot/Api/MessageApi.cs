using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot.Api
{
    class MessageApi
    {
        public static async void SendTextMessage(string message, ITelegramBotClient botClient, MessageEventArgs telegramMessageEvent)
        {
            try {
                await botClient.SendTextMessageAsync(
                    chatId: telegramMessageEvent.Message.Chat,
                    text: message,
                    parseMode: ParseMode.Html
                );
            } catch {
            }

            ConsoleOutputUtilities.MessageOutConsoleMessage(message, telegramMessageEvent);
        }
    }
}
