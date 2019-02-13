
namespace ClunkerBot.Models {
    public class Enums {
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

        public enum PlatePurchaseType {
            Unknown,
            BuyNow, Auction, FutureAuction
        }

        public enum Temperatures {
            Kelvin, Celcius, Fahrenheit
        }

        public enum TransmissionEnum {
            Manual, Automatic, DCT, CVT, AMT,
            Unknown
        }
    }
}