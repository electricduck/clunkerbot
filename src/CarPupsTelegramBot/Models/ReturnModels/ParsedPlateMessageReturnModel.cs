using ClunkerBot.Models;

namespace ClunkerBot.Models.ReturnModels {
    public class ParsedPlateMessageReturnModel {
        public string CountryCode { get; set; }
        public string Flag { get; set; }
        public string Message { get; set; }
        public bool FoundMatch { get; set; }
    }
}