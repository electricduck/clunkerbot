using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels {
    public class AtPlateReturnModel {
        public Enums.AtPlateFormat Format { get; set; }
        public string Location { get; set; }
        public string Special { get; set; }
        public bool Valid { get; set; }
    }
}