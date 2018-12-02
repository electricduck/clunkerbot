
namespace CarPupsTelegramBot.Models {
    public class Enums {
        public enum EngineFuelEnum {
            Petrol, Diesel, Electric
        }

        public enum EngineTypeEnum {
            SC,
            I2, I3, I4, I5, I6,
            V6, V8, V12,
            W12, W16,
            EV
        }

        public enum GbPlateFormat {
            yr1902, // 1902 to 1932
            yr1932, // 1932 to 1953
            yr1953, // 1953 to 1963
            yr1963, // 1963 to 1982
            yr1982, // 1982 to 2001
            yr2001  // 2001 onwards
        }
    }
}