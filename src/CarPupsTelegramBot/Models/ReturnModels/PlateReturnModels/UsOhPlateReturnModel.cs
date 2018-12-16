using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels {
    public class UsOhPlateReturnModel {
        public Enums.UsOhPlateFormat Format { get; set; }
        public string Issue { get; set; }
        public string Special { get; set; }
        public bool Valid { get; set; }
    }
}