using ClunkerBot.Plates.Models;

namespace ClunkerBot.Plates.Models.ReturnModels {
    public class AtPlateReturnModel : PlateReturnModel {
        public Enums.AtPlateFormat Format { get; set; }
        public string Location { get; set; }
        public string Special { get; set; }
        public bool Valid { get; set; }
    }
}