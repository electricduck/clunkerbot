using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels {
    public class FrPlateReturnModel {
        public Enums.FrPlateFormat Format { get; set; }
        public string Issue { get; set; }
        public bool Valid { get; set; }
    }
}