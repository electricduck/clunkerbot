using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels {
    public class UsScPlateReturnModel {
        public Enums.UsScPlateFormat Format { get; set; }
        public bool Valid { get; set; }
    }
}