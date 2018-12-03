using System;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Utilities
{
    class TelegramUtilities
    {
        public static string GetTelegramUser(long TelegramId)
        {
            UserData userData = new UserData();

            UserModel user = userData.GetUserByTelegramId(TelegramId);

            if(String.IsNullOrEmpty(user.TelegramUsername)) {
                return $"<i>{user.TelegramName}</i>";
            } else {
                return $"@{user.TelegramUsername}";
            }

        }
    }
}