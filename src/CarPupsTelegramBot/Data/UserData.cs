using System.Linq;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot.Data
{
    class UserData
    {
        public bool AddUpdateUser(UserModel user)
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

                return true;
            } catch {
                return false;
            }
        }
    }
}