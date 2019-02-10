using ClunkerBot.Plates.Models;

namespace ClunkerBot.Plates.Models.ReturnModels {
    public class RuPlateReturnModel : PlateReturnModel {
        public Enums.RuPlateFormatEnum Format { get; set; }
        public string Location { get; set; }
        public string Special { get; set; }
    }
}