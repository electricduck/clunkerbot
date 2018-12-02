using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels;

namespace CarPupsTelegramBot.Utilities.PlateUtilities
{
    public class GbPlateUtilities
    {
        private static string Year1902Regex = @"^(([A-Z]{1,2})\s?([0-9]{1,4}))$";
        private static string Year1932Regex = @"^(([A-Z]{1})([A-Z]{2})\s?([0-9]{1,4}))$";
        private static string Year1953Regex = @"^(([0-9]{1,4})\s?([A-Z]{1,3}))$";
        private static string Year1963Regex = @"^(([A-Z]{3})\s?([0-9]{1,3})([A-Z]{1}))$";
        private static string Year1979DiplomaticRegex = "^(([0-9]{3})([A-Z]{1})([0-9]{3}))$";
        private static string Year1983Regex = @"^(([A-Z]{1})([0-9]{2,3})\s?([A-Z]{1})([A-Z]{2}))$";
        private static string Year2001Regex = @"(([A-Z]{2,2})([0-9]{2})\s?([A-Z]{3,3}))$";
        private static string Year2015TradeRegex = @"([0-9]{5})";

        public static PlateReturnModel ParseGbPlate(string plate, Enums.GbPlateFormat format)
        {
            PlateReturnModel plateReturn = null;

            plate = plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Year1902Regex)) {
                plateReturn = ParseGbYr1902Plate(plate);
            } else if(Regex.IsMatch(plate, Year1932Regex)) {
                plateReturn = ParseGbYr1932Plate(plate);
            } else if(Regex.IsMatch(plate, Year1953Regex)) {
                plateReturn = ParseGbYr1953Plate(plate);
            } else if(Regex.IsMatch(plate, Year1963Regex)) {
                plateReturn = ParseGbYr1963Plate(plate);
            } else if(Regex.IsMatch(plate, Year1979DiplomaticRegex)) {
                plateReturn = ParseGbYr1979DiplomaticPlate(plate);
            } else if(Regex.IsMatch(plate, Year1983Regex)) {
                plateReturn = ParseGbYr1983Plate(plate);
            } else if(Regex.IsMatch(plate, Year2001Regex)) {
                plateReturn = ParseGbYr2001Plate(plate);
            } else if(Regex.IsMatch(plate, Year2015TradeRegex)) {
                plateReturn = ParseGbYr2015TradePlate(plate);
            } else {
                plateReturn = new PlateReturnModel {
                    Format = Enums.GbPlateFormat.custom
                };
            }

            return plateReturn;
        }
        private static PlateReturnModel ParseGbYr1902Plate(string plate)
        {
            Regex regex = new Regex(Year1902Regex);
            Match match = regex.Match(plate);

            var locationMnemonic = match.Groups[2].Value;
            var issue = match.Groups[3].Value;

            var location = GetPre2001GbLocationMnemonic(locationMnemonic);
            Enums.GbPlatePost2001Type type = Enums.GbPlatePost2001Type.Location;

            if(plate == "LM0") {
                location = "London";
                type = Enums.GbPlatePost2001Type.LordMayorOfLondon;
            } else if(plate == "S0") {
                location = "Scotland";
                type = Enums.GbPlatePost2001Type.LordProvostsOfEdinburgh;
            } else if(plate == "G0") {
                location = "Glasgow";
                type = Enums.GbPlatePost2001Type.LordProvostsOfGlasgow;
            } else if(plate == "RG0") {
                location = "Aberdeen";
                type = Enums.GbPlatePost2001Type.LordProvostsOfAberdeen;
            }

            PlateReturnModel plateReturn = new PlateReturnModel {
                Location = location,
                Issue = Convert.ToInt32(issue),
                Type = type,
                Format = Enums.GbPlateFormat.yr1902
            };

            return plateReturn;
        }

        private static PlateReturnModel ParseGbYr1932Plate(string plate)
        {
            Regex regex = new Regex(Year1932Regex);
            Match match = regex.Match(plate);

            var locationMnemonic = match.Groups[3].Value;
            var issue = match.Groups[4].Value;

            var location = GetPre2001GbLocationMnemonic(locationMnemonic);

            PlateReturnModel plateReturn = new PlateReturnModel {
                Location = location,
                Issue = Convert.ToInt32(issue),
                Format = Enums.GbPlateFormat.yr1932
            };

            return plateReturn;
        }

        private static PlateReturnModel ParseGbYr1953Plate(string plate)
        {
            Regex regex = new Regex(Year1953Regex);
            Match match = regex.Match(plate);

            var locationMnemonic = match.Groups[3].Value;
            var issue = match.Groups[2].Value;

            var location = GetPre2001GbLocationMnemonic(locationMnemonic);

            PlateReturnModel plateReturn = new PlateReturnModel {
                Location = location,
                Issue = Convert.ToInt32(issue),
                Format = Enums.GbPlateFormat.yr1953
            };

            return plateReturn;
        }

        private static PlateReturnModel ParseGbYr1963Plate(string plate)
        {
            Regex regex = new Regex(Year1963Regex);
            Match match = regex.Match(plate);

            var locationMnemonic = match.Groups[2].Value;
            var yearMnemonic = match.Groups[4].Value;

            var location = GetPre2001GbLocationMnemonic(locationMnemonic);
            var year = GetGbSuffixYearMnemonic(Char.Parse(yearMnemonic));

            PlateReturnModel plateReturn = new PlateReturnModel {
                Location = location,
                Year = year,
                Format = Enums.GbPlateFormat.suffix
            };

            return plateReturn;
        }

        private static PlateReturnModel ParseGbYr1979DiplomaticPlate(string plate)
        {
            Regex regex = new Regex(Year1979DiplomaticRegex);
            Match match = regex.Match(plate);

            var diplomaticOrganisationMnemonic = match.Groups[2].Value;
            var diplomaticTypeMnemonic = match.Groups[3].Value;
            var diplomaticRankMnemonic = match.Groups[4].Value;

            var diplomaticOrganisation = GetDiplomaticOrganisationMnemonic(Convert.ToInt32(diplomaticOrganisationMnemonic));
            var diplomaticType = GetDiplomaticTypeMnemonic(Char.Parse(diplomaticTypeMnemonic));
            var diplomaticRank = GetDiplomaticRankMnemonic(Convert.ToInt32(diplomaticRankMnemonic));

            PlateReturnModel plateReturn = new PlateReturnModel {
                DiplomaticOrganisation = diplomaticOrganisation,
                DiplomaticType = diplomaticType,
                DiplomaticRank = diplomaticRank,
                Format = Enums.GbPlateFormat.diplomatic1979
            };

            return plateReturn;
        }

        private static PlateReturnModel ParseGbYr1983Plate(string plate)
        {
            Regex regex = new Regex(Year1983Regex);
            Match match = regex.Match(plate);

            var locationMnemonic = match.Groups[5].Value;
            var yearMnemonic = match.Groups[2].Value;

            var location = GetPre2001GbLocationMnemonic(locationMnemonic);
            var year = GetGbPrefixYearMnemonic(Char.Parse(yearMnemonic));
            var type = Enums.GbPlatePost2001Type.Location;

            if(plate[0] == 'Q') {
                type = Enums.GbPlatePost2001Type.QPlate;
            }

            PlateReturnModel plateReturn = new PlateReturnModel {
                Location = location,
                Year = year,
                Type = type,
                Format = Enums.GbPlateFormat.prefix
            };

            return plateReturn;
        }

        private static PlateReturnModel ParseGbYr2001Plate(string plate)
        {
            Regex regex = new Regex(Year2001Regex);
            Match match = regex.Match(plate);

            var locationMnemonic = match.Groups[2].Value;
            var yearMnemonic = match.Groups[3].Value;

            int yearMnemonicInt = Convert.ToInt32(yearMnemonic);

            var parsedLocationMnemoic = GetPost2001GbLocationMnemonic(locationMnemonic);
            var parsedYearMnemoic = GetGbCurrentYearMnemonic(yearMnemonicInt);

            var location = parsedLocationMnemoic.Location;
            var type = parsedLocationMnemoic.Type;
            var year = parsedYearMnemoic.Year;
            var month = "March";

            if(parsedYearMnemoic.SecondHalfOfYear) {
                month = "September";
            }

            PlateReturnModel plateReturn = new PlateReturnModel {
                Location = location,
                Type = type,
                Year = year,
                Month = month,
                Format = Enums.GbPlateFormat.current
            };

            return plateReturn;
        }

        private static PlateReturnModel ParseGbYr2015TradePlate(string plate)
        {
            PlateReturnModel plateReturn = new PlateReturnModel {
                Issue = Convert.ToInt32(plate),
                Format = Enums.GbPlateFormat.trade2015
            };

            return plateReturn;
        }

        private static string GetPre2001GbLocationMnemonic(string locationMnemonic)
        {
            if(locationMnemonic.Length == 3) {
                locationMnemonic = locationMnemonic.Substring(1);
            }

            if(Pre2001GbLocationMnemonics.ContainsKey(locationMnemonic)) {
                string location;

                Pre2001GbLocationMnemonics.TryGetValue(locationMnemonic, out location);

                return location;
            } else {
                return "<i>Unknown</i>";
            }
        }

        private static GetPost2001GbLocationMnemonicReturnModel GetPost2001GbLocationMnemonic(string locationMnemonic)
        {
            string location = "<i>Unknown</i>";
            Enums.GbPlatePost2001Type type = Enums.GbPlatePost2001Type.Location;

            if(CompareLetters(locationMnemonic, "AA", "AG") || CompareLetters(locationMnemonic, "AM", "AN"))
            {
                location = "Peterborough <i>(Anglia)</i>";
            } else if(CompareLetters(locationMnemonic, "AH", "AL")) {
                type = Enums.GbPlatePost2001Type.Reserved;
            } else if(CompareLetters(locationMnemonic, "AO", "AU")) {
                location = "Norwich <i>(Anglia)</i>";
            } else if(CompareLetters(locationMnemonic, "AV", "AY")) {
                location = "Ipswich <i>(Anglia)</i>";

            } else if(CompareLetters(locationMnemonic, "BA", "BX")) {
                location = "Birmingham";
            } else if(locationMnemonic == "BY") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "CA", "CO")) {
                location = "Cardiff <i>(Cymru)</i>";
            } else if(CompareLetters(locationMnemonic, "CP", "CV")) {
                location = "Swansea <i>(Cymru)</i>";
            } else if(CompareLetters(locationMnemonic, "CW", "CX")) {
                location = "Bangor <i>(Cymru)</i>";
            } else if(locationMnemonic == "CY") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "DA", "DK")) {
                location = "Chester <i>(Deeside)</i>";
            } else if(CompareLetters(locationMnemonic, "DL", "DP") || CompareLetters(locationMnemonic, "DS", "DY")) {
                location = "Shrewsbury <i>(Deeside)</i>";
            } else if(locationMnemonic == "DR") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "EA", "EC") || CompareLetters(locationMnemonic, "EF", "EG") || CompareLetters(locationMnemonic, "EJ", "EY")) {
                location = "Chelmsford <i>(Essex)</i>";
            } else if(locationMnemonic == "ED" || locationMnemonic == "EH") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "FA", "FN") || locationMnemonic == "FP") {
                location = "Nottingham <i>(Forest & Fens)</i>";
            } else if(CompareLetters(locationMnemonic, "FR", "FT") || CompareLetters(locationMnemonic, "FV", "FY")) {
                location = "Lincoln <i>(Forest & Fens)</i>";
            } else if(locationMnemonic == "FO" || locationMnemonic == "FU") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "GA", "GN")) {
                location = "Maidstone <i>(Garden of England)</i>";
            } else if(CompareLetters(locationMnemonic, "GP", "GY")) {
                location = "Brighton <i>(Garden of England)</i>";
            } else if(locationMnemonic == "GO") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "HA", "HJ")) {
                location = "Bournemouth <i>(Hampshire & Dorset)</i>";
            } else if(CompareLetters(locationMnemonic, "HK", "HN") || CompareLetters(locationMnemonic, "HP", "HV") || CompareLetters(locationMnemonic, "HX", "HY")) {
                location = "Portsmouth <i>(Hampshire & Dorset)</i>";
            } else if(locationMnemonic == "HW") {
                location = "Isle of Wight <i>(Hampshire & Dorset)</i>";
            } else if(locationMnemonic == "HO") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "KA", "KL")) {
                location = "Borehamwood / Luton";
            } else if(CompareLetters(locationMnemonic, "KM", "KY")) {
                location = "Northampton";

            } else if(CompareLetters(locationMnemonic, "LA", "LJ")) {
                location = "Wimbledon <i>(London)</i>";
            } else if(CompareLetters(locationMnemonic, "LK", "LT")) {
                location = "Borehamwood / Stanmore <i>(London)</i>";
            } else if(CompareLetters(locationMnemonic, "LU", "LY")) {
                location = "Sidcup <i>(London)</i>";

            } else if(CompareLetters(locationMnemonic, "MA", "MM") || CompareLetters(locationMnemonic, "MT", "MX") || locationMnemonic == "MP") {
                location = "Manchester";
            } else if(locationMnemonic == "MN") {
                location = "Isle of Man";
            } else if(locationMnemonic == "MO" || locationMnemonic == "MR" || locationMnemonic == "MS" || locationMnemonic == "MY") {
                type = Enums.GbPlatePost2001Type.Reserved;
                
            } else if(CompareLetters(locationMnemonic, "NA", "NM")) {
                location = "Newcastle <i>(North)</i>";
            } else if(CompareLetters(locationMnemonic, "NP", "NY")) {
                location = "Stockton <i>(North)</i>";
            } else if(locationMnemonic == "NO") {
                type = Enums.GbPlatePost2001Type.Banned;
            
            } else if(CompareLetters(locationMnemonic, "OA", "OJ") || CompareLetters(locationMnemonic, "OL", "OM") || CompareLetters(locationMnemonic, "OO", "OP") || CompareLetters(locationMnemonic, "OT", "OY")) {
                location = "Oxford";
            } else if(locationMnemonic == "OK" || locationMnemonic == "ON" || locationMnemonic == "OR" || locationMnemonic == "OS") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "PA", "PT")) {
                location = "Preston <i>(Preston)</i>";
            } else if(CompareLetters(locationMnemonic, "PU", "PY")) {
                location = "Caslisle <i>(Preston)</i>";

            } else if(CompareLetters(locationMnemonic, "RA", "RT") || CompareLetters(locationMnemonic, "RV", "RY")) {
                location = "Reading";
            } else if(locationMnemonic == "RU") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "SA", "SJ")) {
                location = "Glasgow <i>(Scotland)</i>";
            } else if(CompareLetters(locationMnemonic, "SK", "SO")) {
                location = "Edinburgh <i>(Scotland)</i>";
            } else if(CompareLetters(locationMnemonic, "SP", "ST")) {
                location = "Dundee <i>(Scotland)</i>";
            } else if(CompareLetters(locationMnemonic, "SV", "SW")) {
                location = "Aberdeen <i>(Scotland)</i>";
            } else if(CompareLetters(locationMnemonic, "SX", "SY")) {
                location = "Inverness";
            } else if(locationMnemonic == "SU") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(locationMnemonic == "TN") {
                location = "Scotland";

            } else if(CompareLetters(locationMnemonic, "VA", "VC") || CompareLetters(locationMnemonic, "VE", "VV")) {
                location = "Worcester <i>(Severn Valley)</i>";
            } else if(locationMnemonic == "VD" || locationMnemonic == "VW") {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "WA", "WB") || CompareLetters(locationMnemonic, "WD", "WJ")) {
                location = "Exeter <i>(West of England)</i>";
            } else if(CompareLetters(locationMnemonic, "WK", "WL")) {
                location = "Truro <i>(West of England)</i>";
            } else if(CompareLetters(locationMnemonic, "WM", "WY")) {
                location = "Bristol <i>(West of England)</i>";
            } else if(locationMnemonic == "C") {
                type = Enums.GbPlatePost2001Type.Banned;
            
            } else if(CompareLetters(locationMnemonic, "XA", "XF")) {
                type = Enums.GbPlatePost2001Type.Export;
            } else if(CompareLetters(locationMnemonic, "XG", "XY")) {
                type = Enums.GbPlatePost2001Type.Reserved;

            } else if(CompareLetters(locationMnemonic, "YA", "YK")) {
                location = "Leeds <i>(Yorkshire)</i>";
            } else if(CompareLetters(locationMnemonic, "YM", "YV")) {
                location = "Sheffield <i>(Yorkshire)</i>";
            } else if(CompareLetters(locationMnemonic, "YX", "YY")) {
                location = "Beverley <i>(Yorkshire)</i>";
            } else if(locationMnemonic == "YL") {
                location = "Leeds / Sheffield <i>(Yorkshire)</i>";
            } else if(locationMnemonic == "YV") {
                location = "Sheffield / Leeds <i>(Yorkshire)</i>";
            }

            GetPost2001GbLocationMnemonicReturnModel getPost2001GbLocationMnemonicReturn = new GetPost2001GbLocationMnemonicReturnModel
            {
                Location = location,
                Type = type
            };

            return getPost2001GbLocationMnemonicReturn;
        }

        private static bool CompareLetters(string letterToCompare, string startLetter, string endLetter)
        {
            if(String.Compare(letterToCompare, startLetter, true) >= 0 && String.Compare(letterToCompare, endLetter, true) <= 0)
            {
                return true;
            } else {
                return false;
            }
        }

        private static int GetGbSuffixYearMnemonic(char yearMnemonic)
        {
            if(GbSuffixYearMnemonics.ContainsKey(yearMnemonic)) {
                int year;

                GbSuffixYearMnemonics.TryGetValue(yearMnemonic, out year);

                return year;
            } else {
                return 0;
            }
        }

        private static int GetGbPrefixYearMnemonic(char yearMnemonic)
        {
            if(GbSuffixYearMnemonics.ContainsKey(yearMnemonic)) {
                int year;

                GbPrefixYearMnemonics.TryGetValue(yearMnemonic, out year);

                return year;
            } else {
                return 0;
            }
        }

        private static string GetDiplomaticTypeMnemonic(char diplomaticTypeMnemonic)
        {
            if(DiplomaticTypeMnemonics.ContainsKey(diplomaticTypeMnemonic)) {
                string diplomaticType;

                DiplomaticTypeMnemonics.TryGetValue(diplomaticTypeMnemonic, out diplomaticType);

                return diplomaticType;
            } else {
                return "<i>Unknown</i>";
            }
        }

        private static string GetDiplomaticOrganisationMnemonic(int diplomaticOrganisationMnemonic)
        {
            string diplomaticOrganisation;

            if(diplomaticOrganisationMnemonic >= 350 && diplomaticOrganisationMnemonic <= 399) {
                diplomaticOrganisation = "<i>Classified</i>";
            } else if(diplomaticOrganisationMnemonic >= 600 && diplomaticOrganisationMnemonic <= 649) {
                diplomaticOrganisation = "Non-British Royalty on visit";
            } else if(DiplomaticOrganisationMnemonics.ContainsKey(diplomaticOrganisationMnemonic)) {
                DiplomaticOrganisationMnemonics.TryGetValue(diplomaticOrganisationMnemonic, out diplomaticOrganisation);
            } else {
                diplomaticOrganisation = "<i>Unknown</i>";
            }

            return diplomaticOrganisation;
        }

        private static string GetDiplomaticRankMnemonic(int diplomaticRankMnemonic)
        {
            string diplomaticRank;

            if(diplomaticRankMnemonic >= 101 && diplomaticRankMnemonic <= 399) {
                diplomaticRank = "Diplomat";
            } else if(diplomaticRankMnemonic >= 400 && diplomaticRankMnemonic <= 699) {
                diplomaticRank = "Non-Diplomatic staff";
            } else if(diplomaticRankMnemonic >= 700 && diplomaticRankMnemonic <= 999) {
                diplomaticRank = "Consular / Other Non-Diplomatic staff";
            } else {
                diplomaticRank = "<i>Unknown</i>";
            }

            return diplomaticRank;
        }

        private static GetGbCurrentYearMnemonicReturnModel GetGbCurrentYearMnemonic(int yearMnemonic)
        {
            int year = 0;
            bool secondHalfOfYear = false;

            if(yearMnemonic > 50) {
                year = yearMnemonic-50;
                year = year+2000;

                secondHalfOfYear = true;
            } else if(yearMnemonic == 0) {
                year = 2050;
                secondHalfOfYear = true;
            } else {
                year = yearMnemonic+2000;
                secondHalfOfYear = false;
            }

            GetGbCurrentYearMnemonicReturnModel getGbCurrentYearMnemonicReturn = new GetGbCurrentYearMnemonicReturnModel
            {
                Year = year,
                SecondHalfOfYear = secondHalfOfYear
            };

            return getGbCurrentYearMnemonicReturn;
        }

        private static Dictionary<string, string> Pre2001GbLocationMnemonics = new Dictionary<string, string>()
        {
            {"A", "London"},
            {"AA", "Bournemouth (Salisbury until 1980)"},
            {"AB", "Worcester"},
            {"AC", "Coventry"},
            {"AD", "Gloucester"},
            {"AE", "Bristol"},
            {"AF", "Truro"},
            {"AG", "Hull (Ayrshire until 1974)"},
            {"AH", "Norwich"},
            {"AJ", "Middlesbrough (Yorkshire (North Riding) until 1974)"},
            {"AK", "Sheffield (Bradford until 1974)"},
            {"AL", "Nottingham"},
            {"AM", "Swindon"},
            {"AN", "West Ham (changed to London from 1967) then again to Reading in 1974. (MAN used only in Isle of Man)"},
            {"AO", "Carlisle"},
            {"AP", "Brighton"},
            {"AR", "Chelmsford (Hertfordshire until 1974)"},
            {"AS", "Inverness"},
            {"AT", "Hull"},
            {"AU", "Nottingham"},
            {"AV", "Peterborough (Aberdeenshire until 1974)"},
            {"AW", "Shrewsbury"},
            {"AX", "Cardiff (Monmouthshire until 1974)"},
            {"AY", "Leicester"},
            {"B", "Lancashire"},
            {"BA", "Manchester (Salford until 1974)"},
            {"BB", "Newcastle upon Tyne"},
            {"BC", "Leicester"},
            {"BD", "Northampton"},
            {"BE", "Lincoln (Grimsby until 1981)"},
            {"BF", "Stoke-on-Trent"},
            {"BG", "Liverpool (Birkenhead until 1974)"},
            {"BH", "Luton (Buckinghamshire until 1974)"},
            {"BJ", "Ipswich (East Suffolk until 1974)"},
            {"BK", "Portsmouth"},
            {"BL", "Reading"},
            {"BM", "Luton"},
            {"BN", "Manchester (Bolton until 1981)"},
            {"BO", "Cardiff"},
            {"BP", "Portsmouth"},
            {"BR", "Newcastle upon Tyne (Durham until 1981)"},
            {"BS", "Aberdeen (Orkney until 1980)"},
            {"BT", "Leeds (Yorkshire (East Riding) until 1974; York until 1981)"},
            {"BU", "Manchester (Oldham until 1974)"},
            {"BV", "Preston (Blackburn until 1974)"},
            {"BW", "Oxford"},
            {"BX", "Haverfordwest (Carmarthenshire until 1974)"},
            {"BY", "Croydon (changed to London (North-West) on closure from 1967)"},
            {"C", "Yorkshire (West Riding)"},
            {"CA", "Chester (Denbighshire until 1974)"},
            {"CB", "Manchester (Blackburn until 1974; Bolton until 1981)"},
            {"CC", "Bangor"},
            {"CD", "Brighton"},
            {"CE", "Peterborough (Cambridge until 1981)"},
            {"CF", "Reading (West Suffolk until 1974)"},
            {"CG", "Bournemouth (Salisbury until 1980)"},
            {"CH", "Nottingham (Derby until 1974)"},
            {"CJ", "Gloucester (Hereford until 1981)"},
            {"CK", "Preston"},
            {"CL", "Norwich"},
            {"CM", "Liverpool (Birkenhead until 1974)"},
            {"CN", "Newcastle upon Tyne (Gateshead until 1974)"},
            {"CO", "Exeter (Plymouth until 1980)"},
            {"CP", "Huddersfield (Halifax until 1974)"},
            {"CR", "Southampton"},
            {"CS", "Glasgow (Ayr until 1981)"},
            {"CT", "Lincoln (Boston until 1981)"},
            {"CU", "Newcastle upon Tyne (South Shields until 1974)"},
            {"CV", "Truro"},
            {"CW", "Preston (Burnley until 1974)"},
            {"CX", "Huddersfield"},
            {"CY", "Swansea (SCY used for Isles of Scilly)"},
            {"D", "Kent"},
            {"DA", "Birmingham (Wolverhampton until 1974)"},
            {"DB", "Manchester (Stockport until 1974)"},
            {"DC", "Middlesbrough"},
            {"DD", "Gloucester"},
            {"DE", "Haverfordwest"},
            {"DF", "Gloucester"},
            {"DG", "Gloucester"},
            {"DH", "Dudley (Walsall until 1974)"},
            {"DJ", "Liverpool (St Helens until 1974; Warrington until 1981)"},
            {"DK", "Manchester (Rochdale until 1974; Bolton until 1981)"},
            {"DL", "Isle of Wight"},
            {"DM", "Chester (Flintshire until 1974)"},
            {"DN", "Leeds (York until 1981)"},
            {"DO", "Lincoln (Boston until 1981)"},
            {"DP", "Reading"},
            {"DR", "Exeter (Plymouth until 1980)"},
            {"DS", "Glasgow (Peeblesshire until 1974)"},
            {"DT", "Sheffield (Doncaster until 1974)"},
            {"DU", "Coventry"},
            {"DV", "Exeter"},
            {"DW", "Cardiff (Newport until 1974)"},
            {"DX", "Ipswich"},
            {"DY", "Brighton (Hastings until 1980)"},
            {"E", "Staffordshire"},
            {"EA", "Dudley (West Bromwich until 1974)"},
            {"EB", "Peterborough (Cambridge until 1981)"},
            {"EC", "Preston (Westmorland until 1974; Kendal until 1981)"},
            {"ED", "Liverpool (Warrington until 1981)"},
            {"EE", "Lincoln (Grimsby until 1981)"},
            {"EF", "Middlesbrough (West Hartlepool until 1974)"},
            {"EG", "Peterborough"},
            {"EH", "Stoke-on-Trent"},
            {"EJ", "Haverfordwest (Cardiganshire until 1974; Aberystwyth until 1981)"},
            {"EK", "Liverpool (Wigan until 1974; Warrington until 1981)"},
            {"EL", "Bournemouth"},
            {"EM", "Liverpool (Bootle until 1974)"},
            {"EN", "Manchester (Bury until 1974; Bolton until 1981)"},
            {"EO", "Preston (Barrow-in-Furness until 1981)"},
            {"EP", "Swansea (Montgomeryshire until 1974)"},
            {"ER", "Peterborough (Cambridge until 1981)"},
            {"ES", "Dundee (Perthshire until 1974)"},
            {"ET", "Sheffield (Rotherham until 1974)"},
            {"EU", "Bristol (Breconshire until 1974)"},
            {"EV", "Chelmsford"},
            {"EW", "Peterborough"},
            {"EX", "Norwich (Great Yarmouth until 1974)"},
            {"EY", "Bangor (Anglesey until 1974)"},
            {"F", "Essex"},
            {"FA", "Stoke-on-Trent (Burton-upon-Trent until 1974)"},
            {"FB", "Bristol (Bath until 1974)"},
            {"FC", "Oxford"},
            {"FD", "Dudley"},
            {"FE", "Lincoln"},
            {"FF", "Bangor (Merionethshire until 1974; Aberystwyth until 1981)"},
            {"FG", "Brighton (Fife until 1974)"},
            {"FH", "Gloucester"},
            {"FJ", "Exeter"},
            {"FK", "Dudley (Worcester until 1974)"},
            {"FL", "Peterborough"},
            {"FM", "Chester"},
            {"FN", "Maidstone (Canterbury until 1981)"},
            {"FO", "Gloucester (Radnorshire until 1974; Hereford for Radnorshire until 1981)"},
            {"FP", "Leicester (Rutland until 1974)"},
            {"FR", "Preston (Blackpool until 1974)"},
            {"FS", "Edinburgh"},
            {"FT", "Newcastle upon Tyne (Tynemouth until 1974)"},
            {"FU", "Lincoln (Grimsby until 1981)"},
            {"FV", "Preston (Blackpool until 1974)"},
            {"FW", "Lincoln"},
            {"FX", "Bournemouth"},
            {"FY", "Liverpool (Southport until 1974)"},
            {"G", "Glasgow"},
            {"GA", "Glasgow"},
            {"GB", "Glasgow"},
            {"GC", "London (South-West)"},
            {"GD", "Glasgow"},
            {"GE", "Glasgow"},
            {"GF", "London (South-West)"},
            {"GG", "Glasgow"},
            {"GH", "London (South-West)"},
            {"GJ", "London (South-West)"},
            {"GK", "London (South-West)"},
            {"GL", "Truro (Bath until 1974)"},
            {"GM", "Reading (Motherwell and Wishaw until 1974)"},
            {"GN", "London (South-West)"},
            {"GO", "London (South-West)"},
            {"GP", "London (South-West)"},
            {"GR", "Newcastle upon Tyne (Durham until 1981)"},
            {"GS", "Luton (Perthshire until 1974)"},
            {"GT", "London (South-West)"},
            {"GU", "London (South-East)"},
            {"GV", "Ipswich (West Suffolk until 1974)"},
            {"GW", "London (South-East)"},
            {"GX", "London (South-East)"},
            {"GY", "London (South-East)"},
            {"H", "London"},
            {"HA", "Dudley (Smethwick until 1974)"},
            {"HB", "Cardiff (Merthyr Tydfil until 1974)"},
            {"HC", "Brighton (Eastbourne until 1974; Hastings until 1980)"},
            {"HD", "Huddersfield (Dewsbury until 1974)"},
            {"HE", "Sheffield (Barnsley until 1974)"},
            {"HF", "Liverpool (Wallasey until 1974)"},
            {"HG", "Preston (Burnley until 1974)"},
            {"HH", "Carlisle"},
            {"HJ", "Chelmsford (Southend-on-Sea until 1974)"},
            {"HK", "Chelmsford"},
            {"HL", "Sheffield (Wakefield until 1974)"},
            {"HM", "East Ham ( changed to London (Cent) from 1967)"},
            {"HN", "Middlesbrough (Darlington until 1974)"},
            {"HO", "Bournemouth (Salisbury until 1980)"},
            {"HP", "Coventry"},
            {"HR", "Swindon"},
            {"HS", "Glasgow (Renfrewshire until 1974)"},
            {"HT", "Bristol"},
            {"HU", "Bristol"},
            {"HV", "East Ham (Changed to London (Cent) from 1967"},
            {"HW", "Bristol"},
            {"HX", "London (Central) (Middlesex before 1965)"},
            {"HY", "Bristol"},
            {"J", "Durham"},
            {"JA", "Manchester (Stockport until 1974)"},
            {"JB", "Reading"},
            {"JC", "Bangor"},
            {"JD", "West Ham (Changed to London (Cent) from 1967"},
            {"JE", "Peterborough (Cambridge until 1981)"},
            {"JF", "Leicester"},
            {"JG", "Maidstone (Canterbury until 1981)"},
            {"JH", "Reading (Hertfordshire until 1974)"},
            {"JJ", "Maidstone (London until 1974; Canterbury until 1981)"},
            {"JK", "Brighton (Eastbourne until 1974; Hastings until 1980)"},
            {"JL", "Lincoln (Boston until 1981)"},
            {"JM", "Reading (Westmorland until 1974)"},
            {"JN", "Chelmsford (Southend-on-Sea until 1974)"},
            {"JO", "Oxford"},
            {"JP", "Liverpool (Wigan until 1974; Warrington until 1981)"},
            {"JR", "Newcastle upon Tyne"},
            {"JS", "Ross-Shire"},
            {"JT", "Bournemouth"},
            {"JU", "Leicester"},
            {"JV", "Lincoln (Grimsby until 1981)"},
            {"JW", "Birmingham (Wolverhampton until 1974)"},
            {"JX", "Huddersfield (Halifax until 1974)"},
            {"JY", "Exeter (Plymouth until 1980)"},
            {"K", "Liverpool"},
            {"KA", "Liverpool"},
            {"KB", "Liverpool"},
            {"KC", "Liverpool"},
            {"KD", "Liverpool"},
            {"KE", "Maidstone"},
            {"KF", "Liverpool"},
            {"KG", "Cardiff"},
            {"KH", "Hull"},
            {"KJ", "Maidstone"},
            {"KK", "Maidstone"},
            {"KL", "Maidstone"},
            {"KM", "Maidstone"},
            {"KN", "Maidstone"},
            {"KO", "Maidstone"},
            {"KP", "Maidstone"},
            {"KR", "Maidstone"},
            {"KS", "Edinburgh (Roxburghshire until 1947 Selkirk until 1980)"},
            {"KT", "Maidstone (Canterbury until 1981)"},
            {"KU", "Sheffield (Bradford until 1974)"},
            {"KV", "Coventry"},
            {"KW", "Sheffield (Bradford until 1974)"},
            {"KX", "Luton (Buckinghamshire until 1974)"},
            {"KY", "Sheffield (Bradford until 1974)"},
            {"L", "Glamorganshire"},
            {"LA", "London (North-West) (used for London County Council before 1965)"},
            {"LB", "London (North-West)"},
            {"LC", "London (North-West)"},
            {"LD", "London (North-West)"},
            {"LE", "London (North-West)"},
            {"LF", "London (North-West)"},
            {"LG", "Chester"},
            {"LH", "London (North-West)"},
            {"LJ", "Bournemouth"},
            {"LK", "London (North-West)"},
            {"LL", "London (North-West)"},
            {"LM", "London (North-West)"},
            {"LN", "London (North-West)"},
            {"LO", "London (North-West)"},
            {"LP", "London (North-West)"},
            {"LR", "London (North-West)"},
            {"LS", "Edinburgh (Stirling until 1981)"},
            {"LT", "London (North-West)"},
            {"LU", "London (North-West)"},
            {"LV", "Liverpool"},
            {"LW", "London (North-West)"},
            {"LX", "London (North-West)"},
            {"LY", "London (North-West)"},
            {"M", "Cheshire"},
            {"MA", "Chester"},
            {"MB", "Chester"},
            {"MC", "London (North-East) ( Middlesex before 1965)"},
            {"MD", "London (North-East) (Middlesex before 1965)"},
            {"ME", "London (North-East) (Middlesex before 1965)"},
            {"MF", "London (North-East) (Middlesex before 1965)"},
            {"MG", "London (North-East) (Middlesex before 1965)"},
            {"MH", "London (North-East) (Middlesex before 1965)"},
            {"MJ", "Luton"},
            {"MK", "London (North-East) (Middlesex before 1965)"},
            {"ML", "London (North-East) (Middlesex before 1965)"},
            {"MM", "London (North-East) (Middlesex before 1965)"},
            {"MN", "Isle of Man"},
            {"MO", "Reading"},
            {"MP", "London (North-East) (Middlesex before 1965)"},
            {"MR", "Swindon"},
            {"MS", "Edinburgh (Stirling until 1981)"},
            {"MT", "London (North-East) (Middlesex before 1965)"},
            {"MU", "London (North-East) (Middlesex before 1965)"},
            {"MV", "London (South-East) (Middlesex before 1965)"},
            {"MW", "Swindon"},
            {"MX", "London (South-East) (Middlesex before 1965)"},
            {"MY", "London (South-East) (Middlesex before 1965)"},
            {"N", "Manchester"},
            {"NA", "Manchester"},
            {"NB", "Manchester"},
            {"NC", "Manchester"},
            {"ND", "Manchester"},
            {"NE", "Manchester"},
            {"NF", "Manchester"},
            {"NG", "Norwich"},
            {"NH", "Northampton"},
            {"NJ", "Brighton"},
            {"NK", "Luton (Hertfordshire until 1974)"},
            {"NL", "Newcastle upon Tyne"},
            {"NM", "Luton"},
            {"NN", "Nottingham"},
            {"NO", "Chelmsford"},
            {"NP", "Worcester"},
            {"NR", "Leicester"},
            {"NS", "Glasgow (Sutherland until 1974)"},
            {"NT", "Shrewsbury"},
            {"NU", "Nottingham"},
            {"NV", "Northampton"},
            {"NW", "Leeds"},
            {"NX", "Dudley"},
            {"NY", "Cardiff"},
            {"O", "Birmingham"},
            {"OA", "Birmingham"},
            {"OB", "Birmingham"},
            {"OC", "Birmingham"},
            {"OD", "Exeter"},
            {"OE", "Birmingham"},
            {"OF", "Birmingham"},
            {"OG", "Birmingham"},
            {"OH", "Birmingham"},
            {"OJ", "Birmingham"},
            {"OK", "Birmingham"},
            {"OL", "Birmingham"},
            {"OM", "Birmingham"},
            {"ON", "Birmingham"},
            {"OO", "Chelmsford"},
            {"OP", "Birmingham"},
            {"OR", "Portsmouth"},
            {"OS", "Glasgow (Wigtownshire until 1974; Stranraer until 1981)"},
            {"OT", "Portsmouth"},
            {"OU", "Bristol (Hampshire until 1974)"},
            {"OV", "Birmingham"},
            {"OW", "Southampton"},
            {"OX", "Birmingham"},
            {"OY", "Croydon (changed to London (NW) from 1967)"},
            {"P", "Surrey"},
            {"PA", "Guildford"},
            {"PB", "Guildford"},
            {"PC", "Guildford"},
            {"PD", "Guildford"},
            {"PE", "Guildford"},
            {"PF", "Guildford"},
            {"PG", "Guildford"},
            {"PH", "Guildford"},
            {"PJ", "Guildford"},
            {"PK", "Guildford"},
            {"PL", "Guildford"},
            {"PM", "Guildford"},
            {"PN", "Brighton"},
            {"PO", "Portsmouth (GPO formerly used for General Post Office vehicles)"},
            {"PP", "Luton (Buckinghamshire until 1974)"},
            {"PR", "Bournemouth"},
            {"PS", "Aberdeen (Lerwick until 1980)"},
            {"PT", "Newcastle upon Tyne (Durham until 1981)"},
            {"PU", "Chelmsford"},
            {"PV", "Ipswich"},
            {"PW", "Norwich"},
            {"PX", "Portsmouth"},
            {"PY", "Middlesbrough (Yorkshire (North Riding) until 1974)"},
            {"R", "Derbyshire"},
            {"RA", "Nottingham"},
            {"RB", "Nottingham"},
            {"RC", "Nottingham (Derby until 1974)"},
            {"RD", "Reading"},
            {"RE", "Stoke-on-Trent"},
            {"RF", "Stoke-on-Trent"},
            {"RG", "Newcastle upon Tyne (Aberdeen until 1974)"},
            {"RH", "Hull"},
            {"RJ", "Manchester (Salford until 1974)"},
            {"RK", "Croydon (changed to London (NW) from 1967"},
            {"RL", "Truro"},
            {"RM", "Carlisle"},
            {"RN", "Preston"},
            {"RO", "Luton (Hertfordshire until 1974)"},
            {"RP", "Northampton"},
            {"RR", "Nottingham"},
            {"RS", "Aberdeen"},
            {"RT", "Ipswich (East Suffolk until 1974)"},
            {"RU", "Bournemouth"},
            {"RV", "Portsmouth"},
            {"RW", "Coventry"},
            {"RX", "Reading"},
            {"RY", "Leicester"},
            {"S", "Edinburgh"},
            {"SA", "Aberdeen"},
            {"SB", "Argyll until 1974 then Oban until 1980, then Glasgow from 1981"},
            {"SC", "Edinburgh"},
            {"SD", "Glasgow (Ayr until 1981)"},
            {"SE", "Aberdeen (Keith until 1981)"},
            {"SF", "Edinburgh"},
            {"SG", "Edinburgh"},
            {"SH", "Edinburgh (Selkirk until 1980)"},
            {"SJ", "Glasgow (Bute until 1974 Ayr until 1981)"},
            {"SK", "Caithness"},
            {"SL", "Dundee (Clackmannanshire until 1974)"},
            {"SM", "Carlisle (Dumfries until 1981)"},
            {"SN", "Dundee (Dunbartonshire until 1974)"},
            {"SO", "Aberdeen"},
            {"SP", "Dundee (Fife until 1974)"},
            {"SR", "Dundee"},
            {"SS", "East Lothian (Haddingtonshire until 1921 Aberdeen until 1974)"},
            {"ST", "Inverness"},
            {"SU", "Glasgow (Kincardineshire until 1974)"},
            {"SV", "Kinross-shire (until 1974), subsequently unused"},
            {"SW", "Carlisle (Kirkcudbrightshire until 1974 Dumfries until 1981)"},
            {"SX", "Edinburgh"},
            {"SY", "Midlothian (until 1974), subsequently unused"},
            {"T", "Devon"},
            {"TA", "Exeter"},
            {"TB", "Liverpool (Lancashire until 1974; Warrington until 1981)"},
            {"TC", "Bristol (Lancashire until 1974)"},
            {"TD", "Manchester (Lancashire until 1974; Bolton until 1981)"},
            {"TE", "Manchester (Lancashire until 1974; Bolton until 1981)"},
            {"TF", "Reading (Lancashire until 1974)"},
            {"TG", "Cardiff"},
            {"TH", "Swansea"},
            {"TJ", "Liverpool (Lancashire until 1974)"},
            {"TK", "Exeter (Plymouth until 1980)"},
            {"TL", "Lincoln"},
            {"TM", "Luton"},
            {"TN", "Newcastle upon Tyne"},
            {"TO", "Nottingham"},
            {"TP", "Portsmouth"},
            {"TR", "Southampton"},
            {"TS", "Dundee"},
            {"TT", "Exeter"},
            {"TU", "Chester"},
            {"TV", "Nottingham"},
            {"TW", "Chelmsford"},
            {"TX", "Cardiff"},
            {"TY", "Newcastle upon Tyne"},
            {"U", "Leeds"},
            {"UA", "Leeds"},
            {"UB", "Leeds"},
            {"UC", "London (Central)"},
            {"UD", "Oxford"},
            {"UE", "Dudley"},
            {"UF", "Brighton"},
            {"UG", "Leeds"},
            {"UH", "Cardiff"},
            {"UJ", "Shrewsbury"},
            {"UK", "Birmingham (Wolverhampton until 1974)"},
            {"UL", "London (Central)"},
            {"UM", "Leeds"},
            {"UN", "Denbighshire prior to 1974, Barnstable 1974–80, Exeter from 1981"},
            {"UO", "Exeter (Barnstaple until 1980)"},
            {"UP", "Newcastle upon Tyne (Durham until 1981)"},
            {"UR", "Luton (Hertfordshire until 1974)"},
            {"US", "Glasgow"},
            {"UT", "Leicester"},
            {"UU", "London (Central)"},
            {"UV", "London (Central)"},
            {"UW", "London (Central)"},
            {"UX", "Shrewsbury"},
            {"UY", "Worcester"},
            {"V", "Lanarkshire"},
            {"VA", "Peterborough (Lanarkshire until 1974; Cambridge until 1981)"},
            {"VB", "Croydon (changed to London from 1967 until 1974) then Canterbury then Maidstone from 1981"},
            {"VC", "Coventry"},
            {"VD", "Lanarkshire (until 1974), later Luton"},
            {"VE", "Peterborough (Cambridge until 1981)"},
            {"VF", "Norwich"},
            {"VG", "Norwich"},
            {"VH", "Huddersfield"},
            {"VJ", "Gloucester (Hereford until 1981)"},
            {"VK", "Newcastle upon Tyne"},
            {"VL", "Lincoln"},
            {"VM", "Manchester"},
            {"VN", "Middlesbrough (Yorkshire (North Riding) until 1974)"},
            {"VO", "Nottingham"},
            {"VP", "Birmingham"},
            {"VR", "Manchester"},
            {"VS", "Luton (Greenock until 1974)"},
            {"VT", "Stoke-on-Trent"},
            {"VU", "Manchester"},
            {"VV", "Northampton"},
            {"VW", "Chelmsford"},
            {"VX", "Chelmsford"},
            {"VY", "Leeds (York until 1981)"},
            {"W", "Sheffield"},
            {"WA", "Sheffield"},
            {"WB", "Sheffield"},
            {"WC", "Chelmsford"},
            {"WD", "Dudley"},
            {"WE", "Sheffield"},
            {"WF", "Sheffield (Yorkshire (East Riding) until 1974)"},
            {"WG", "Sheffield (Stirlingshire until 1974)"},
            {"WH", "Manchester (Bolton until 1981)"},
            {"WJ", "Sheffield"},
            {"WK", "Coventry"},
            {"WL", "Oxford"},
            {"WM", "Liverpool (Southport until 1974)"},
            {"WN", "Swansea"},
            {"WO", "Cardiff (Monmouthshire until 1974)"},
            {"WP", "Worcester"},
            {"WR", "Leeds"},
            {"WS", "Bristol (Edinburgh until 1974)"},
            {"WT", "Leeds"},
            {"WU", "Leeds"},
            {"WV", "Brighton (Wiltshire until 1974)"},
            {"WW", "Leeds"},
            {"WX", "Leeds"},
            {"WY", "Leeds"},
            {"X", "Northumberland"},
            {"XA", "London. (Kirkaldy 1964–74 with year suffix)"},
            {"XB", "London (Coatbridge 1964–1974 with year suffix)"},
            {"XC", "London (Solihull 1964–1974 with year suffix)"},
            {"XD", "London (Luton 1964–1974 with year suffix)"},
            {"XE", "London (Luton 1964–1974 with year suffix)"},
            {"XF", "London (Torbay 1964–1974 with year suffix)"},
            {"XG", "Middlesbrough (until 1974), subsequently unused"},
            {"XH", "London"},
            {"XJ", "Manchester (until 1974), subsequently unused"},
            {"XK", "London"},
            {"XL", "London"},
            {"XM", "London"},
            {"XN", "London"},
            {"XO", "London"},
            {"XP", "London, later temporary plates for vehicles being exported to Europe"},
            {"XR", "London"},
            {"XS", "Paisley (until 1974), subsequently unused"},
            {"XT", "London"},
            {"XU", "London"},
            {"XV", "London"},
            {"XW", "London"},
            {"XX", "London"},
            {"XY", "London"},
            {"Y", "Somerset"},
            {"YA", "Taunton"},
            {"YB", "Taunton"},
            {"YC", "Taunton"},
            {"YD", "Taunton"},
            {"YE", "London (Central)"},
            {"YF", "London (Central)"},
            {"YG", "Leeds"},
            {"YH", "London (Central)"},
            {"YJ", "Brighton (Dundee until 1974)"},
            {"YK", "London (Central)"},
            {"YL", "London (Central)"},
            {"YM", "London (Central)"},
            {"YN", "London (Central)"},
            {"YO", "London (Central)"},
            {"YP", "London (Central)"},
            {"YR", "London (Central)"},
            {"YS", "Glasgow"},
            {"YT", "London (Central)"},
            {"YU", "London (Central)"},
            {"YV", "London (Central)"},
            {"YW", "London (Central)"},
            {"YX", "London (Central)"},
            {"YY", "London (Central)"}
        };

        private static Dictionary<char, int> GbSuffixYearMnemonics = new Dictionary<char, int>()
        {
            {'A', 1963},
            {'B', 1964},
            {'C', 1965},
            {'D', 1966},
            {'E', 1967},
            {'F', 1967},
            {'G', 1968},
            {'H', 1969},
            {'J', 1970},
            {'K', 1971},
            {'L', 1972},
            {'M', 1973},
            {'N', 1974},
            {'P', 1975},
            {'R', 1976},
            {'S', 1977},
            {'T', 1978},
            {'V', 1979},
            {'W', 1980},
            {'X', 1981},
            {'Y', 1982}
        };

        private static Dictionary<char, int> GbPrefixYearMnemonics = new Dictionary<char, int>()
        {
            {'A', 1983},
            {'B', 1984},
            {'C', 1985},
            {'D', 1986},
            {'E', 1987},
            {'F', 1988},
            {'G', 1989},
            {'H', 1990},
            {'J', 1991},
            {'K', 1992},
            {'L', 1993},
            {'M', 1994},
            {'N', 1995},
            {'P', 1996},
            {'R', 1997},
            {'S', 1998},
            {'T', 1999},
            {'V', 1999},
            {'W', 2000},
            {'X', 2000},
            {'Y', 2001}
        };

        private static Dictionary<char, string> DiplomaticTypeMnemonics = new Dictionary<char, string>()
        {
            {'D', "London"},
            {'X', "International"}
        };

        private static Dictionary<int, string> DiplomaticOrganisationMnemonics = new Dictionary<int, string>()
        {
            {270, "🇺🇸 United States"},
            {271, "🇺🇸 United States"},
            {272, "🇺🇸 United States"},
            {273, "🇺🇸 United States"},
            {274, "🇺🇸 United States"},
            {900, "Commonwealth Secretariat"},
            {901, "🇪🇺 European Commission"},
            {902, "🇪🇺 Council of Europe"},
            {903, "European Centre for Medium-Range Weather Forecasts"},
            {904, "North-East Atlantic Fisheries Commission"},
            {905, "🇪🇺 European Parliament"},
            {906, "Inter-American Development Bank"},
            {907, "🇺🇳 International Maritime Organization"},
            {908, "International Cocoa Organisation"},
            {909, "International Coffee Organisation"},
            {910, "International Finance Corporation"},
            {911, "International Labour Organization"},
            {912, "International Sugar Organisation"},
            {913, "European Police College"},
            {914, "International Whaling Commission"},
            {915, "International Wheat Council"},
            {916, "North Atlantic Treaty Organisation"},
            {917, "🇺🇳 United Nations"},
            {918, "🇪🇺 Western European Union"},
            {919, "🇺🇳 World Health Organization"},
            {920, "Eastern Caribbean Commission"},
            {921, "Joint European Torus"},
            {922, "International Oil Pollution Compensation Fund"},
            {923, "International Maritime Satellite Organisation"},
            {924, "Commonwealth Foundation"},
            {925, "International Maritime Organization"},
            {926, "Commonwealth Telecommunications Bureau"},
            {927, "United Nations High Commissioner for Refugees"},
            {928, "Commonwealth Agricultural Bureau"},
            {929, "International Lead and Zinc Corporation"},
            {930, "Oslo and Paris Commissions"},
            {931, "Joint European Torus"},
            {932, "North Atlantic Salmon Conservation Organization"},
            {933, "European Investment Bank"},
            {934, "European Telecommunications Satellite Organisation"},
            {935, "European School (Oxford)"},
            {936, "African Development Bank"},
            {937, "European Bank for Reconstruction and Development"},
            {938, "European Bank for Reconstruction and Development"},
            {940, "European Bioinformatics Institute"},
            {941, "European Medicines Agency"},
            {944, "European Banking Authority"}
        };
    }
}