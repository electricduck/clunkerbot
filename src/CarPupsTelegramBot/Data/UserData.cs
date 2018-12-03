using System;
using System.Linq;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot.Data
{
    class UserData
    {
        public void AddUpdateUser(UserModel user)
        {
            try {
                using (var db = new CarPupsTelegramBotContext())
                {
                    int dbCount = 0;

                    var result = db.Users.SingleOrDefault(u => u.TelegramId == user.TelegramId);
                    
                    if (result != null)
                    {
                        result.TelegramName = user.TelegramName;
                        result.TelegramUsername = user.TelegramUsername;
                        
                        dbCount = db.SaveChanges();
                    } else {
                        db.Users.Add(user);
                        dbCount = db.SaveChanges();
                    }

                    ConsoleOutputUtilities.DatabaseSaveConsoleMessage(dbCount);
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString()); 
            }
        }
    
        public UserModel GetUserByTelegramId(long telegramId)
        {
            try {
                using (var db = new CarPupsTelegramBotContext())
                {
                    var result = db.Users.SingleOrDefault(u => u.TelegramId == telegramId);

                    return result;
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
                return null;
            }
        }

        public UserModel GetUserByTelegramUsername(string telegramUsername)
        {
            try {
                using (var db = new CarPupsTelegramBotContext())
                {
                    var result = db.Users.SingleOrDefault(u => u.TelegramUsername == telegramUsername);

                    return result;
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
                return null;
            }
        }
    }
}