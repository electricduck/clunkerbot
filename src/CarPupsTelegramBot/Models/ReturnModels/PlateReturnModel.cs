using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Models.ReturnModels {
    public class PlateReturnModel {
        public string Location { get; set; }

        public int Year { get; set; }
        public string Month { get; set; }
        public int Issue { get; set; }
        public Enums.GbPlatePost2001Type Type { get; set; }
        public Enums.GbPlateFormat Format { get; set; }
        public string DiplomaticOrganisation { get; set; }
        public string DiplomaticType { get; set; }
        public string DiplomaticRank { get; set; }
    }
}