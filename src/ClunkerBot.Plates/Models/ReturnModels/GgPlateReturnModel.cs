
namespace ClunkerBot.Plates.Models.ReturnModels {
    public class GgPlateReturnModel : PlateReturnModel {
        public Enums.GgPlateFormatEnum Format { get; set; }
        public string Issue { get; set; }
        public string Special { get; set; }
    }
}