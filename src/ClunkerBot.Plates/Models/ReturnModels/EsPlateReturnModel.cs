using ClunkerBot.Plates.Models;

namespace ClunkerBot.Plates.Models.ReturnModels {
    public class EsPlateReturnModel : PlateReturnModel {
        public Enums.EsPlateFormatEnum Format { get; set; }
        public string Location { get; set; }
        public string Special { get; set; }
    }
}