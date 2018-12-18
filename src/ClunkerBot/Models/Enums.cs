
namespace ClunkerBot.Models {
    public class Enums {
        public enum AtPlateFormat {
            unspecified,
            yr1990,
            yr1990Official
        }

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

        public enum FrPlateFormat {
            unspecified,
            yr1950,         // 1950 to 2009 / FNI
            yr2009          // 2009 onwards / SIV
        }

        public enum GbPlateFormat {
            unspecified,
            yr1902,         // 1902 to 1932
            yr1932,         // 1932 to 1953
            yr1953,         // 1953 to 1963
            suffix,         // 1963 to 1982 / Suffix
            prefix,         // 1982 to 2001 / Prefix
            current,        // 2001 to 2051 / Current
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

        public enum NlPlateFormat {
            unspecified,
            yr1898,     // 1898 to 1951
            yr1951,     // 1951 to 1956 / Side Code 1
            yr1965,     // 1965 to 1973 / Side Code 2
            yr1973,     // 1973 to 1978 / Side Code 3
            yr1978,     // 1978 to 1991 / Side Code 4
            yr1991,     // 1991 to 1999 / Side Code 5
            yr1999,     // 1999 to 2008 / Side Code 6
            yr2006,     // 2006 onwards / Side Code 7
            yr2006b,    // 2006 onwards / Side Code 8
            yr2006c,    // 2006 onwards / Side Code 9
            yr2011,     // 2011 to 2015 / Side Code 10
            yr2015,     // 2015 onwards / Side Code 11
            yr2016,     // 2016 onwards / Side Code 13
        }

        public enum PlatePurchaseType {
            Unknown,
            BuyNow, Auction, FutureAuction
        }

        public enum TransmissionEnum {
            Manual, Automatic, DCT, CVT, AMT,
            Unknown
        }

        public enum UsOhPlateFormat {
            unspecified,
            yr2004,
            yr2015Bike,
            duiOffender
        }

        public enum UsScPlateFormat {
            unspecified,
            yr2008
        }
    }
}