using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClunkerBot.Plates.Models;
using ClunkerBot.Plates.Models.ReturnModels;

// TODO: Add support for diplomatic and military plates
// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_Russia

namespace ClunkerBot.Plates
{
    public class RuPlate
    {
        private static string Standard1993Regex = @"^(([A|B|E|K|M|H|O|P|C|T|Y|X]{0,2})([0-9]{3,4})([A|B|E|K|M|H|O|P|C|T|Y|X]{0,2})([0-9]{2,3})RUS)$";
        private static string Standard1993PoliceRegex = @"^(([A|Y|O])([0-9]{4})([0-9]{2,3})RUS)$";
        private static string Standard1993PublicTransportRegex = "^(([A|B|E|K|M|H|O|P|C|T|Y|X]{2})([0-9]{3})([0-9]{2,3})RUS)$";
        private static string Standard1993TrailerRegex = "^(([A|B|E|K|M|H|O|P|C|T|Y|X]{2})([0-9]{4})([0-9]{2,3})RUS)$";

        public static RuPlateReturnModel ParseRuPlate(string plate)
        {
            RuPlateReturnModel plateReturn = null;

            plate = plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Standard1993Regex))
            {
                plateReturn = ParseStandard1993Plate(plate);
            }
            else
            {
                plateReturn = new RuPlateReturnModel
                {
                    Valid = false
                };
            }

            plateReturn.CountryFlag = "🇷🇺";
            plateReturn.CountryCode = "ru";

            return plateReturn;
        }

        private static RuPlateReturnModel ParseStandard1993Plate(string plate)
        {
            Regex regex = new Regex(Standard1993Regex);
            Match match = regex.Match(plate);

            string locationCode = match.Groups[5].Value;

            string locationString = GetLocationCode(locationCode);
            string specialString = "";

            if(Regex.IsMatch(plate, Standard1993PoliceRegex))
            {
                Regex standard1993PoliceRegex = new Regex(Standard1993PoliceRegex);
                Match standard1993PoliceMatch = standard1993PoliceRegex.Match(plate);

                var policeType = standard1993PoliceMatch.Groups[2].Value;

                switch(policeType)
                {
                    case "A":
                        specialString = "Police (Traffic)";
                        break;
                    case "Y":
                        specialString = "Police (Patrol)";
                        break;
                    case "O":
                        specialString = "Police";
                        break;
                }
            }

            RuPlateReturnModel returnModel = new RuPlateReturnModel
            {
                Format = Enums.RuPlateFormatEnum.Standard1993,
                Location = locationString,
                Special = specialString,
                Valid = true
            };

            return returnModel;
        }

        private static string GetLocationCode(string code)
        {
            if(LocationCodes.ContainsKey(code))
            {
                string decodedLocation;
                LocationCodes.TryGetValue(code, out decodedLocation);
                return decodedLocation;  
            }
            else
            {
                return "";
            }
        }

        private static Dictionary<string, string> LocationCodes = new Dictionary<string, string>()
        {
            {"01", "Republic of Adygea"},
            {"02", "Republic of Bashkortostan"},
            {"102", "Republic of Bashkortostan"},
            {"03", "Republic of Buryatia"},
            {"04", "Altai Republic"},
            {"05", "Republic of Dagestan"},
            {"06", "Republic of Ingushetia"},
            {"07", "Kabardino-Balkar Republic"},
            {"08", "Republic of Kalmykia"},
            {"09", "Karachay-Charkess Republic"},
            {"10", "Republic of Karelia"},
            {"11", "Komi Republic"},
            {"12", "Mari El Republic"},
            {"13", "Republic of Mordovia"},
            {"113", "Republic of Mordovia"},
            {"14", "Sakha Republic"},
            {"15", "Republic of North Ossetia–Alania"},
            {"16", "Republic of Tatarstan"},
            {"116", "Republic of Tatarstan"},
            {"716", "Republic of Tatarstan"},
            {"17", "Tuva Republic"},
            {"18", "Udmurt Republic"},
            {"19", "Republic of Khakassia"},
            {"20", "Chechen Republic"},
            {"95", "Chechen Republic"},
            {"21", "Chuvash Republic"},
            {"121", "Chuvash Republic"},
            {"22", "Altai Krai"},
            {"23", "Krasnodar Krai"},
            {"93", "Krasnodar Krai"},
            {"123", "Krasnodar Krai"},
            {"24", "Krasnoyarsk Krai"},
            {"124", "Krasnoyarsk Krai"},
            {"25", "Primorsky Krai"},
            {"125", "Primorsky Krai"},
            {"26", "Stavropol Krai"},
            {"126", "Stavropol Krai"},
            {"27", "Khabarovsk Krai"},
            {"28", "Amur Oblast"},
            {"29", "Arkhangelsk Oblast"},
            {"30", "Astrakhan Oblast"},
            {"31", "Belgorod Oblast"},
            {"32", "Bryansk Oblast"},
            {"33", "Vladimir Oblast"},
            {"34", "Volgograd Oblast"},
            {"134", "Volgograd Oblast"},
            {"35", "Vologda Oblast"},
            {"36", "Voronezh Oblast"},
            {"136", "Voronezh Oblast"},
            {"37", "Ivanovo Oblast"},
            {"38", "Irkutsk Oblast"},
            {"138", "Irkutsk Oblast"},
            {"39", "Kaliningrad Oblast"},
            {"91", "Kaliningrad Oblast"},
            {"40", "Kaluga Oblast"},
            {"41", "Kamchatka Krai"},
            {"42", "Kemerovo Oblast"},
            {"142", "Kemerovo Oblast"},
            {"43", "Kirov Oblast"},
            {"44", "Kostroma Oblast"},
            {"45", "Kurgan Oblast"},
            {"46", "Kursk Oblast"},
            {"47", "Liningrad Oblast"},
            {"48", "Lipetsk Oblast"},
            {"49", "Magadan Oblast"},
            {"50", "Moscow Oblast"},
            {"90", "Moscow Oblast"},
            {"150", "Moscow Oblast"},
            {"190", "Moscow Oblast"},
            {"750", "Moscow Oblast"},
            {"51", "Murmansk Oblast"},
            {"52", "Nizhny Novgorod Oblast"},
            {"152", "Nizhny Novgorod Oblast"},
            {"53", "Novgorod Oblast"},
            {"54", "Novosibirsk Oblast"},
            {"154", "Novosibirsk Oblast"},
            {"55", "Omsk Oblast"},
            {"56", "Orenburg Oblast"},
            {"57", "Oryol Oblast"},
            {"58", "Penza Oblast"},
            {"59", "Perm Krai"},
            {"159", "Perm Krai"},
            {"60", "Pskov Oblast"},
            {"61", "Rostov Oblast"},
            {"161", "Rostov Oblast"},
            {"62", "Ryazan Oblast"},
            {"63", "Samara Oblast"},
            {"163", "Samara Oblast"},
            {"763", "Samara Oblast"},
            {"64", "Saratov Oblast"},
            {"164", "Saratov Oblast"},
            {"65", "Sakhalin Oblast"},
            {"66", "Sverdlovsk Oblast"},
            {"96", "Sverdlovsk Oblast"},
            {"196", "Sverdlovsk Oblast"},
            {"67", "Smolensk Oblast"},
            {"68", "Tambov Oblast"},
            {"69", "Tver Oblast"},
            {"70", "Tomsk Oblast"},
            {"71", "Tula Oblast"},
            {"72", "Tyumen Oblast"},
            {"73", "Ulyanovsk Oblast"},
            {"173", "Ulyanovsk Oblast"},
            {"74", "Chelyabinsk Oblast"},
            {"174", "Chelyabinsk Oblast"},
            {"75", "Zabaykalsky Krai"},
            {"76", "Yaroslavl Oblast"},
            {"77", "Moscow"},
            {"97", "Moscow"},
            {"99", "Moscow"},
            {"177", "Moscow"},
            {"197", "Moscow"},
            {"199", "Moscow"},
            {"777", "Moscow"},
            {"799", "Moscow"},
            {"78", "St. Petersburg"},
            {"98", "St. Petersburg"},
            {"178", "St. Petersburg"},
            {"198", "St. Petersburg"},
            {"79", "Jewish Autonomous Oblast"},
            {"80", "Agin-Buryat Okrug / Zabaykalsky Krai"},
            {"81", "Komi-Permyak Okrug / Perm Krai"},
            {"82", "Republic of Crimea / Kamchatka Krai"},
            {"83", "Nenets Autonomous Okrug"},
            {"84", "Taymyr Autonomous Okrug / Krasnoyarsk Krai"},
            {"85", "Ust-Orda Buryat Okrug / Irkutsk Oblast"},
            {"86", "Khanty-Mansi Autonomous Okrug"},
            {"186", "Khanty-Mansi Autonomous Okrug"},
            {"87", "Chukotka Autonomous Okrug"},
            {"88", "Evenk Autonomous Okrug / Krasnoyarsk Krai"},
            {"89", "Yamalo-Nenets Autonomous Okrug"},
            {"92", "Sevastopol"},
            {"94", "Outer Russia Federation"}
        };
    }
}