using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels {
    public class NlPlateReturnModel {
        public Enums.NlPlateFormat Format { get; set; }
        public int Issue { get; set; }
        public string Location { get; set; }
        public string Special { get; set; }
        public bool Valid { get; set; }
        public string Year { get; set; }
    }
}