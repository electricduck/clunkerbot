using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using CarPupsTelegramBot.Models.ReturnModels;
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
            } catch (Exception e) {
                Console.WriteLine("⚠️ " + e);
            }

            ConsoleOutputUtilities.MessageOutConsoleMessage(message, telegramMessageEvent);
        }

        public static async void SendPhotoMessage(ImageMessageReturnModel messageReturnModel, ITelegramBotClient botClient, MessageEventArgs telegramMessageEvent) {
            try {
                await botClient.SendPhotoAsync(
                    chatId: telegramMessageEvent.Message.Chat,
                    parseMode: ParseMode.Html,
                    caption: messageReturnModel.Caption,
                    photo: messageReturnModel.PhotoUrl
                );
            } catch (Exception e) {
                Console.WriteLine("⚠️ " + e);
            }
        }
    }
}
