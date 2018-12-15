
namespace CarPupsTelegramBot.Models {
    public class Enums {
        public enum DePlateFormat {
            unspecified,
            yr1956,         // 1956 onwards / Current
            diplomatic1956  // Diplomatic (1956 onwards)
        }

        public enum DriveTypeEnum {
            FWD, RWD, AWD,
            Unknown
        }

        public enum EngineFuelEnum {
            Petrol, Diesel, Electric,
            Unknown
        }

        public enum EngineTypeEnum {
            SC,
            I2, I3, I4, I5, I6,
            V6, V8, V12,
            W12, W16,
            EV,
            Unknown
        }

        public enum GbPlateFormat {
            unspecified,
            yr1902,         // 1902 to 1932
            yr1932,         // 1932 to 1953
            yr1953,         // 1953 to 1963
            suffix,         // 1963 to 1982 / Suffix
            prefix,         // 1982 to 2001 / Prefix
            current,        // 2001 to 2051 / Current
            custom,         // Non-standard private
            trade2015,      // Trade (2015 onwards)
            diplomatic1979  // Diplomatic (1979 onwards)
        }

        public enum GbPlateSpecial {
            Location,
            Reserved,
            Banned,
            Export,
            QPlate,
            LordMayorOfLondon,
            LordProvostsOfEdinburgh,
            LordProvostsOfGlasgow,
            LordProvostsOfAberdeen
        }

        public enum PlatePurchaseType {
            Unknown,
            BuyNow, Auction, FutureAuction
        }

        public enum TransmissionEnum {
            Manual, Automatic, DCT, CVT, AMT,
            Unknown
        }
    }
}