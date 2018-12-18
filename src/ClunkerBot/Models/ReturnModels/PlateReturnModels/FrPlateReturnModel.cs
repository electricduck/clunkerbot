using ClunkerBot.Models;

namespace ClunkerBot.Models.ReturnModels.PlateReturnModels {
    public class FrPlateReturnModel {
        public Enums.FrPlateFormat Format { get; set; }
        public string Issue { get; set; }
        public string Location { get; set; }
        public bool Valid { get; set; }
        public string Year { get; set; }
    }
}