using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClunkerBot.Models;
using ClunkerBot.Models.ReturnModels.PlateReturnModels;

// TODO: Add special-use plates
//       https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_the_Netherlands

namespace ClunkerBot.Utilities.PlateUtilities
{
    public class NlPlateUtilities
    {
        private static string Year1898Regex = @"^(([A|B|D|E|G|H|K|M|N|L|P]{1})([Z|X]{0,1})([0-9]{1,5}))$";
        private static string Year1951Regex = @"^(([A-Z]{2})-([0-9]{2})-([0-9]{2}))$";
        private static string Year1965Regex = @"^(([0-9]{2})-([0-9]{2})-([A-Z]{2}))$";
        private static string Year1973Regex = @"^(([0-9]{2})-([A-Z]{2})-([0-9]{2}))$";
        private static string Year1978Regex = @"^(([A-Z]{2})-([0-9]{2})-([A-Z]{2}))$";
        private static string Year1991Regex = @"^(([A-Z]{2})-([A-Z]{2})-([0-9]{2}))$";
        private static string Year1999Regex = @"^(([0-9]{2})-([A-Z]{1})([A-Z]{1})-([A-Z]{2}))$";
        private static string Year2006Regex = @"^(([0-9]{2})-([A-Z]{1})([A-Z]{2})-([0-9]{1}))$";
        private static string Year2006BRegex = @"^(([0-9]{1})-([A-Z]{1})([A-Z]{2})-([0-9]{2}))$";
        private static string Year2006CRegex = @"^(([A-Z]{1})([A-Z]{1})-([0-9]{3})-([A-Z]{1}))$";
        private static string Year2011Regex = @"^(([A-Z]{1})-([0-9]{3})-([A-Z]{2}))$";
        private static string Year2015Regex = @"^(([A-Z]{1})([A-Z]{2})-([0-9]{2})-([A-Z]{1}))$";

        public static NlPlateReturnModel ParseNlPlate(string plate)
        {
            NlPlateReturnModel plateReturn = null;

            plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Year1898Regex)) {
                plateReturn = ParseNlYr1898Plate(plate);
            } else if(Regex.IsMatch(plate, Year1951Regex)) {
                plateReturn = ParseNlYr1951Plate(plate);
            } else if(Regex.IsMatch(plate, Year1965Regex)) {
                plateReturn = ParseNlYr1965Plate(plate);
            } else if(Regex.IsMatch(plate, Year1973Regex)) {
                plateReturn = ParseNlYr1973Plate(plate);
            } else if(Regex.IsMatch(plate, Year1978Regex)) {
                plateReturn = ParseNlYr1978Plate(plate);
            } else if(Regex.IsMatch(plate, Year1991Regex)) {
                plateReturn = ParseNlYr1991Plate(plate);
            } else if(Regex.IsMatch(plate, Year1999Regex)) {
                plateReturn = ParseNlYr1999Plate(plate);
            } else if(Regex.IsMatch(plate, Year2006Regex)) {
                plateReturn = ParseNlYr2006Plate(plate);
            } else if(Regex.IsMatch(plate, Year2006BRegex)) {
                plateReturn = ParseNlYr2006BPlate(plate);
            } else if(Regex.IsMatch(plate, Year2006CRegex)) {
                plateReturn = ParseNlYr2006CPlate(plate);
            } else if(Regex.IsMatch(plate, Year2011Regex)) {
                plateReturn = ParseNlYr2011Plate(plate);
            } else if(Regex.IsMatch(plate, Year2015Regex)) {
                plateReturn = ParseNlYr2015Plate(plate);
            } else {
                plateReturn = new NlPlateReturnModel {
                    Valid = false
                };
            }

            return plateReturn;
        }

        public static NlPlateReturnModel ParseNlYr1898Plate(string plate)
        {
            Regex regex = new Regex(Year1898Regex);
            Match match = regex.Match(plate);

            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr1898,
                Issue = Convert.ToInt32(match.Groups[4].Value),
                Valid = true
            };

            string locationMnemonic = match.Groups[2].Value;

            switch(locationMnemonic) {
                case "A":
                    returnModel.Location = "Groningen";
                    break;
                case "B":
                    returnModel.Location = "Friesland";
                    break;
                case "D":
                    returnModel.Location = "Drenthe";
                    break;
                case "E":
                    returnModel.Location = "Overijssel";
                    break;
                case "G":
                    returnModel.Location = "Noord Holland";
                    break;
                case "H":
                    returnModel.Location = "Zuid Holland";
                    break;
                case "K":
                    returnModel.Location = "Zeeland";
                    break;
                case "M":
                    returnModel.Location = "Gelderland";
                    break;
                case "N":
                    returnModel.Location = "Noord Brabant";
                    break;
                case "L":
                    returnModel.Location = "Utrecht";
                    break;
                case "P":
                    returnModel.Location = "Limburg";
                    break;
                default:
                    returnModel.Valid = false;
                    break;
            }

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr1951Plate(string plate)
        {
            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr1951,
                Valid = true
            };

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr1965Plate(string plate)
        {
            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr1965,
                Valid = true
            };

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr1973Plate(string plate)
        {
            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr1973,
                Valid = true
            };

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr1978Plate(string plate)
        {
            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr1978,
                Valid = true
            };

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr1991Plate(string plate)
        {
            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr1991,
                Valid = true
            };

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr1999Plate(string plate)
        {
            Regex regex = new Regex(Year1999Regex);
            Match match = regex.Match(plate);

            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr1999,
                Valid = true
            };

            string yearMnemonic = match.Groups[3].Value;

            switch(yearMnemonic) {
                case "D":
                    returnModel.Year = "1999/2000";
                    break;
                case "F":
                    returnModel.Year = "2000";
                    break;
                case "G":
                    returnModel.Year = "2000/2001";
                    break;
                case "H":
                    returnModel.Year = "2001/2002";
                    break;
                case "J":
                    returnModel.Year = "2002";
                    break;
                case "L":
                    returnModel.Year = "2002/2003";
                    break;
                case "N":
                    returnModel.Year = "2003/2004";
                    break;
                case "P":
                    returnModel.Year = "2004/2005";
                    break;
                case "R":
                    returnModel.Year = "2005";
                    break;
                case "S":
                    returnModel.Year = "2005/2006";
                    break;
                case "T":
                    returnModel.Year = "2006/2007";
                    break;
                case "X":
                    returnModel.Year = "2007";
                    break;
                case "Z":
                    returnModel.Year = "2007/2008";
                    break;
                case "M":
                    returnModel.Year = "2011";
                    break;
                case "V":
                    returnModel.Year = "1998-2001";
                    break;
                case "B":
                    returnModel.Year = "2001-2006";
                    break;
                case "W":
                    returnModel.Year = "2008";
                    break;
                default:
                    returnModel.Valid = false;
                    break;
            };

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr2006Plate(string plate)
        {
            Regex regex = new Regex(Year2006Regex);
            Match match = regex.Match(plate);

            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr2006,
                Valid = true
            };

            string yearMnemonic = match.Groups[3].Value;

            switch(yearMnemonic) {
                case "G":
                    returnModel.Year = "2008";
                    break;
                case "H":
                    returnModel.Year = "2008/2009";
                    break;
                case "J":
                    returnModel.Year = "2009";
                    break;
                case "K":
                    returnModel.Year = "2009/2010";
                    break;
                case "L":
                    returnModel.Year = "2010";
                    break;
                case "N":
                    returnModel.Year = "2010/2011";
                    break;
                case "P":
                case "R":
                case "S":
                    returnModel.Year = "2011";
                    break;
                case "T":
                case "X":
                    returnModel.Year = "2012";
                    break;
                case "Z":
                    returnModel.Year = "2012/2013";
                    break;
                case "V":
                    returnModel.Year = "2006-2009";
                    break;
                case "B":
                    returnModel.Year = "2012";
                    break;
                case "D":
                    returnModel.Year = "2005/2006";
                    break;
                case "F":
                    returnModel.Year = "2006";
                    break;
                default:
                    returnModel.Valid = false;
                    break;
            }

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr2006BPlate(string plate)
        {
            Regex regex = new Regex(Year2006BRegex);
            Match match = regex.Match(plate);

            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr2006b,
                Valid = true
            };

            string yearMnemonic = match.Groups[3].Value;

            switch(yearMnemonic) {
                case "K":
                case "S":
                    returnModel.Year = "2013";
                    break;
                case "T":
                    returnModel.Year = "2013/2014";
                    break;
                case "X":
                    returnModel.Year = "2014";
                    break;
                case "Z":
                    returnModel.Year = "2014/2015";
                    break;
                case "V":
                    returnModel.Year = "2009-2012";
                    break;
                default:
                    returnModel.Special = "Export";
                    break;
            }

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr2006CPlate(string plate)
        {
            Regex regex = new Regex(Year2006CRegex);
            Match match = regex.Match(plate);

            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr2006c,
                Valid = true
            };

            string yearMnemonic = match.Groups[2].Value;

            switch(yearMnemonic) {
                case "G":
                    returnModel.Year = "2015";
                    break;
                case "H":
                    returnModel.Year = "2015/2016";
                    break;
                case "J":
                case "K":
                    returnModel.Year = "2015";
                    break;
                case "N":
                    returnModel.Year = "2016/2017";
                    break;
                case "P":
                    returnModel.Year = "2017";
                    break;
                case "R":
                    returnModel.Year = "2017/2018";
                    break;
                case "S":
                case "T":
                    returnModel.Year = "2018";
                    break;
                case "X":
                    returnModel.Year = "2018/2019";
                    break;
                case "Z":
                    returnModel.Year = "2019";
                    break;
                case "V":
                    returnModel.Year = "2012-2016";
                    break;
                case "D":
                    returnModel.Year = "2006";
                    break;
                case "F":
                    returnModel.Year = "2006-2008";
                    break;
                default:
                    returnModel.Valid = false;
                    break;
            }

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr2011Plate(string plate)
        {
            Regex regex = new Regex(Year2011Regex);
            Match match = regex.Match(plate);

            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr2011,
                Valid = true
            };

            string yearMnemonic = match.Groups[2].Value;

            switch(yearMnemonic) {
                case "D":
                    returnModel.Year = "2008-2011";
                    break;
                case "F":
                    returnModel.Year = "2011-2015";
                    break;
                default:
                    returnModel.Valid = false;
                    break;
            }

            return returnModel;
        }

        public static NlPlateReturnModel ParseNlYr2015Plate(string plate)
        {
            Regex regex = new Regex(Year2015Regex);
            Match match = regex.Match(plate);

            NlPlateReturnModel returnModel = new NlPlateReturnModel {
                Format = Enums.NlPlateFormat.yr2015,
                Valid = true
            };

            string yearMnemonic = match.Groups[2].Value;

            switch(yearMnemonic) {
                case "D":
                    returnModel.Year = "2015";
                    break;
                default:
                    returnModel.Valid = false;
                    break;
            }

            return returnModel;
        }
    }
}