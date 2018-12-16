using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels {
    public class DePlateReturnModel {
        public Enums.DePlateFormat Format { get; set; }
        public string Location { get; set; }
        public string Special { get; set; }
        public bool Valid { get; set; }
    }
}