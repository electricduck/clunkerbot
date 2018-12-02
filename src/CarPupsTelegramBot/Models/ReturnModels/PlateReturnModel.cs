using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Models.ReturnModels {
    public class PlateReturnModel {
        public string Location { get; set; }

        public int Year { get; set; }
        public int Issue { get; set; }
        public Enums.GbPlateFormat Format { get; set; }
    }
}