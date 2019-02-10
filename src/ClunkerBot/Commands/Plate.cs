using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Telegram.Bot.Args;
using ClunkerBot.Api;
using ClunkerBot.Data;
using ClunkerBot.Models;
using ClunkerBot.Models.ReturnModels;
using ClunkerBot.Models.ReturnModels.PlateReturnModels;
using ClunkerBot.Plates;
using ClunkerBot.Plates.Models.ReturnModels;
using ClunkerBot.Utilities;
using ClunkerBot.Utilities.PlateUtilities;

namespace ClunkerBot.Commands
{
    public class Plate
    {
        public static string Parse(string plate, string country = "")
        {
            try {
                string countryCodeUnsupportedMessage = $@"#️⃣ <i>Parse Plate:</i> ❓ <code>{plate}</code>
—
<i>Country code '{country}' is currenty unsupported.</i>";

                plate = plate.ToUpper();

                if(country.Length == 2) {
                    country = country.ToLower().Substring(0, 2);

                    switch(country) {
                        case "at":
                            var parsedAtPlate = ParseAtPlate(plate);
                            return parsedAtPlate.Message;
                        case "de":
                            var parsedDePlate = ParseDePlate(plate);
                            return parsedDePlate.Message;
                        case "fr":
                            var parsedFrPlate = ParseFrPlate(plate);
                            return parsedFrPlate.Message;
                        case "gb":
                        case "uk":
                            var parsedGbPlate = ParseGbPlate(plate);
                            return parsedGbPlate.Message;
                        case "gg":
                            var parsedGgPlate = ParseGgPlate(plate);
                            return parsedGgPlate.Message;
                        case "nl":
                            var parsedNlPlate = ParseNlPlate(plate);
                            return parsedNlPlate.Message;
                        case "ru":
                            var parsedRuPlate = ParseRuPlate(plate);
                            return parsedRuPlate.Message;
                        case "us":
                            var parsedUsPlate = ParseAnyPlate(plate, true);
                            
                            if(parsedUsPlate.FoundMatch) {
                                return parsedUsPlate.Message;
                            } else {
                        return $@"#️⃣ <i>Parse Plate:</i> 🇺🇸 <code>{plate}</code>
—
<i>No match for plate found.</i>
<i>The state may not be supported — check the help (</i><code>/help parseplate</code><i>) for a list of supported states.</i>";
                            }
                        default:
                            return countryCodeUnsupportedMessage;
                    }
                } else if(country.Length == 5) {
                    switch(country) {
                        case "us-oh":
                            var parsedUsOhPlate = ParseUsOhPlate(plate);
                            return parsedUsOhPlate.Message;
                        case "us-sc":
                            var parsedUsScPlate = ParseUsScPlate(plate);
                            return parsedUsScPlate.Message;
                        default:
                            return countryCodeUnsupportedMessage;
                    }
                } else {
                    var parsedPlate = ParseAnyPlate(plate);

                    if(parsedPlate.FoundMatch) {
                        return parsedPlate.Message;
                    } else {
                        return $@"#️⃣ <i>Parse Plate:</i> ❓ <code>{plate}</code>
—
<i>No match for plate found.</i>
<i>The country may not be supported — check the help (</i><code>/help parseplate</code><i>) for a list of supported countries.</i>";
                    }
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
                return HelpData.GetHelp("parseplate", false);
            }
        }

        private static ParsedPlateMessageReturnModel ParseAnyPlate(string plate, bool usOnly = false)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = null;
            List<ParsedPlateMessageReturnModel> matches = new List<ParsedPlateMessageReturnModel>();

            if(!usOnly) {
                var parsedAtPlate = ParseAtPlate(plate);
                var parsedDePlate = ParseDePlate(plate);
                var parsedFrPlate = ParseFrPlate(plate);
                var parsedGbPlate = ParseGbPlate(plate);
                var parsedGgPlate = ParseGgPlate(plate);
                var parsedNlPlate = ParseNlPlate(plate);
                var parsedRuPlate = ParseRuPlate(plate);

                if(parsedAtPlate.FoundMatch) { matches.Add(parsedAtPlate); }
                if(parsedDePlate.FoundMatch) { matches.Add(parsedDePlate); }
                if(parsedFrPlate.FoundMatch) { matches.Add(parsedFrPlate); }
                if(parsedGbPlate.FoundMatch) { matches.Add(parsedGbPlate); }
                if(parsedGgPlate.FoundMatch) { matches.Add(parsedGgPlate); }
                if(parsedNlPlate.FoundMatch) { matches.Add(parsedNlPlate); }
                if(parsedRuPlate.FoundMatch) { matches.Add(parsedRuPlate); }
            }

            var parsedUsOhPlate = ParseUsOhPlate(plate);
            var parsedUsScPlate = ParseUsScPlate(plate);

            if(parsedUsOhPlate.FoundMatch) { matches.Add(parsedUsOhPlate); }
            if(parsedUsScPlate.FoundMatch) { matches.Add(parsedUsScPlate); }

            if(matches.Count == 0) {
                parsedPlateReturn = new ParsedPlateMessageReturnModel {
                    FoundMatch = false
                };
            } else if(matches.Count == 1) {
                parsedPlateReturn = matches[0];
            } else {
                string multipleMatchesMessage = "";

                foreach(var match in matches) {
                    multipleMatchesMessage += $@"• <b>{match.Flag} {match.CountryCode.ToUpper()}:</b> <code>/parseplate {plate} {match.CountryCode.ToLower()}</code>
";
                }

                parsedPlateReturn = new ParsedPlateMessageReturnModel {
                    FoundMatch = true,
                    Message = $@"#️⃣ <i>Parse Plate:</i> ❓ <code>{plate}</code>
—
<b>Multiple matches found</b>
{multipleMatchesMessage}
<i>Type one of the commands to specify the country.</i>"
                };
            }

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseAtPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            AtPlateReturnModel plateReturn = AtPlate.ParseAtPlate(plate);

            parsedPlateReturn.CountryCode = plateReturn.CountryCode;
            parsedPlateReturn.Flag = plateReturn.CountryFlag;

            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            string locationString;
            string specialString;

            if(plateReturn.Valid) {
                if(String.IsNullOrEmpty(plateReturn.Location)) {
                    locationString = "<i>Unknown</i>";
                } else {
                    locationString = plateReturn.Location;
                }

                if(String.IsNullOrEmpty(plateReturn.Special)) {
                    specialString = "<i>No</i>";
                } else {
                    specialString = plateReturn.Special;
                }

                if(plateReturn.Format == Enums.AtPlateFormatEnum.yr1990) {
                    output += $@"<b>Reg. Office:</b> {locationString}
<b>Special:</b> {specialString}
<b>Format:</b> 1990 onwards";
                }
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Austrian plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseDePlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            DePlateReturnModel plateReturn = DePlate.ParseDePlate(plate);

            parsedPlateReturn.Flag = "🇩🇪";
        
            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            string locationString;
            string specialString;

            if(plateReturn.Valid) {
                if(plateReturn.Format == Enums.DePlateFormatEnum.yr1956) {
                    if(String.IsNullOrEmpty(plateReturn.Location)) {
                        locationString = "<i>Unknown</i>";
                    } else {
                        locationString = plateReturn.Location;
                    }

                    if(String.IsNullOrEmpty(plateReturn.Special)) {
                        specialString = "<i>No</i>";
                    } else {
                        specialString = plateReturn.Special;
                    }

                    output += $@"<b>Location:</b> {locationString}
<b>Special:</b> {specialString}
<b>Format:</b> Current <i>(1956 onwards)</i>";
                } else if(plateReturn.Format == Enums.DePlateFormatEnum.diplomatic1956) {
                    output += $@"Format: Diplomatic
                    
<i>This is a diplomatic plate, found on cars used by foreign embassies, high commissions, consulates and international organisations. The cars themselves are usually not personally owned.</i>";
                } 
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported German plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "de";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseFrPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            FrPlateReturnModel plateReturn = FrPlateUtilities.ParseFrPlate(plate);

            parsedPlateReturn.Flag = "🇫🇷";

            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            string locationString;

            if(plateReturn.Valid) {
                if(String.IsNullOrEmpty(plateReturn.Location)) {
                    locationString = "<i>Unknown</i>";
                } else {
                    locationString = plateReturn.Location;
                }

                if(plateReturn.Format == Enums.FrPlateFormat.yr1950) {
                    output += $@"<b>Reg. Department:</b> {locationString}
<b>Format:</b> FNI <i>(1950 to 2009)</i>";
                } else if(plateReturn.Format == Enums.FrPlateFormat.yr2009) {
                    output += $@"<b>Issue No.:</b> {plateReturn.Issue} <i>(approx.)</i>
<b>Year:</b> {plateReturn.Year} <i>(approx.)</i>*
<b>Format:</b> SIV <i>(2009 onwards)</i>

<b>*</b> <i>This is based on a steady average amount of cars per year, providing the SIV system lasts the estimated 80 years initially planned.</i>";
                }
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported French plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "fr";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseGbPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            ClunkerBot.Models.ReturnModels.PlateReturnModel plateReturn = GbPlateUtilities.ParseGbPlate(plate, Enums.GbPlateFormat.unspecified);

            parsedPlateReturn.Flag = "🇬🇧";

            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            string yearString;

            if(plateReturn.Year == 0) {
                yearString = "<i>Unknown</i>";
            } else {
                yearString = plateReturn.Year.ToString();
            }

            if(plateReturn.Valid) {
                if(plateReturn.Format == Enums.GbPlateFormat.yr1902) {
                    var specialString = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlateSpecial.LordMayorOfLondon) {
                        specialString = "Lord Mayor of London";
                    } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfAberdeen) {
                        specialString = "Lord Provosts Of Aberdeen";
                    } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfEdinburgh) {
                        specialString = "Lord Provosts of Edinburgh";
                    } else if(plateReturn.Type == Enums.GbPlateSpecial.LordProvostsOfGlasgow) {
                        specialString = "Lord Provosts of Glasgow";
                    }

                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Issue No.:</b> {plateReturn.Issue}
<b>Special:</b> {specialString}
<b>Format:</b> 1902 to 1932";
                } else if(plateReturn.Format == Enums.GbPlateFormat.yr1932) {
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Issue No.:</b> {plateReturn.Issue}
<b>Format:</b> 1932 to 1963";
                } else if(plateReturn.Format == Enums.GbPlateFormat.yr1953) {
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Issue No.:</b> {plateReturn.Issue}
<b>Format:</b> 1932 to 1963 (Reversed)";
                } else if(plateReturn.Format == Enums.GbPlateFormat.suffix) {
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString}
<b>Format:</b> Suffix <i>(1963 to 1982)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.prefix) {
                    var specialString = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlateSpecial.QPlate) {
                        specialString = "Q Plate";
                    }

                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Prefix <i>(1983 to 2001)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.current) {
                    var specialString = "<i>No</i>";

                    if(plateReturn.Type == Enums.GbPlateSpecial.Reserved) {
                        specialString = "Reserved";
                    } else if(plateReturn.Type == Enums.GbPlateSpecial.Export) {
                        specialString = "Personal Export";
                    }
                    
                    output += $@"<b>DVLA Office:</b> {plateReturn.Location}
<b>Year Reg.:</b> {yearString} ({plateReturn.Month})
<b>Special:</b> {specialString}
<b>Format:</b> Current <i>(2001 to 2051)</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.trade2015) {
                    output += $@"<b>Issue No.:</b> {plateReturn.Issue}
<b>Format:</b> Trade <i>(2015 onwards)</i>

<i>This is a trade plate (2015 onwards), licensed to motor traders and vehicle testers, permitting the use of an untaxed vehicle on the public highway with certain restrictions.</i>";
                } else if(plateReturn.Format == Enums.GbPlateFormat.diplomatic1979) {
                    output += $@"<b>Diplomatic Org.:</b> {plateReturn.DiplomaticOrganisation}
<b>Diplomatic Type:</b> {plateReturn.DiplomaticType}
<b>Diplomatic Rank:</b> {plateReturn.DiplomaticRank}
<b>Format:</b> Diplomatic

<i>This is a diplomatic plate, found on cars used by foreign embassies, high commissions, consulates and international organisations. The cars themselves are usually not personally owned.</i>";
                } 
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Great Britain plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "gb";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }
    
        private static ParsedPlateMessageReturnModel ParseGgPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            GgPlateReturnModel plateReturn = GgPlate.ParseGgPlate(plate);

            parsedPlateReturn.CountryCode = plateReturn.CountryCode;
            parsedPlateReturn.Flag = plateReturn.CountryFlag;

            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            if(plateReturn.Valid) {
                output += $"<b>Issue No.:</b> {plate}";
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Guersney plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseNlPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            NlPlateReturnModel plateReturn = NlPlateUtilities.ParseNlPlate(plate);

            parsedPlateReturn.Flag = "🇳🇱";
        
            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            string specialString;
            string yearString;

            if(String.IsNullOrEmpty(plateReturn.Special)) {
                specialString = "<i>No</i>";
            } else {
                specialString = plateReturn.Special;
            }

            if(String.IsNullOrEmpty(plateReturn.Year)) {
                yearString = "<i>Unknown</i>";
            } else {
                yearString = plateReturn.Year;
            }

            if(plateReturn.Valid) {
                if(plateReturn.Format == Enums.NlPlateFormat.yr1898) {
                    output += $@"<b>Location:</b> {plateReturn.Location}
<b>Issue:</b> {plateReturn.Issue}
<b>Format:</b> 1989 to 1951";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1951) {
                    output += $@"<b>Format:</b> Side Code 1 <i>(1951 to 1956)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1965) {
                    output += $@"<b>Format:</b> Side Code 2 <i>(1965 to 1973)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1973) {
                    output += $@"<b>Format:</b> Side Code 3 <i>(1973 to 1978)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1978) {
                    output += $@"<b>Format:</b> Side Code 4 <i>(1978 to 1991)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1991) {
                    output += $@"<b>Format:</b> Side Code 5 <i>(1991 to 1999)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr1999) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 6 <i>(1999 to 2008)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2006) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 7 <i>(2006 onwards)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2006b) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 8 <i>(2006 onwards)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2006c) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 9 <i>(2006 onwards)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2011) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 10 <i>(2011 to 2015)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2015) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 11 <i>(2015 onwards)</i>";
                } else if(plateReturn.Format == Enums.NlPlateFormat.yr2016) {
                    output += $@"<b>Year:</b> {yearString}
<b>Special:</b> {specialString}
<b>Format:</b> Side Code 13 <i>(2016 onwards)</i>";
                }
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Netherlands plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "nl";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseRuPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            RuPlateReturnModel plateReturn = RuPlate.ParseRuPlate(plate);

            parsedPlateReturn.CountryCode = plateReturn.CountryCode;
            parsedPlateReturn.Flag = plateReturn.CountryFlag;

            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            string locationString;
            string specialString;

            if(plateReturn.Valid) {
                if(String.IsNullOrEmpty(plateReturn.Location)) {
                    locationString = "<i>Unknown</i>";
                } else {
                    locationString = plateReturn.Location;
                }

                if(String.IsNullOrEmpty(plateReturn.Special)) {
                    specialString = "<i>No</i>";
                } else {
                    specialString = plateReturn.Special;
                }

                if(plateReturn.Format == Enums.RuPlateFormatEnum.Standard1993) {
                    output += $@"<b>Region:</b> {locationString}
<b>Special:</b> {specialString}
<b>Format:</b> 1993 onwards";
                }
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Russian plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseUsOhPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            UsOhPlateReturnModel plateReturn = UsOhPlateUtilities.ParseUsOhPlate(plate);

            parsedPlateReturn.Flag = "🇺🇸";
        
            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            string specialString;
            string usState = "Ohio";

            if(String.IsNullOrEmpty(plateReturn.Special)) {
                specialString = "<i>No</i>";
            } else {
                specialString = $@"Reserved for <i>{plateReturn.Special}</i>";
            }

            if(plateReturn.Valid) {
                if(plateReturn.Format == Enums.UsOhPlateFormat.yr2004) {
                    output += $@"<b>Special:</b> {specialString}
<b>US State:</b> {usState}
<b>Format:</b> 2004 onwards";
                } else if(plateReturn.Format == Enums.UsOhPlateFormat.yr2015Bike) {
                    output += $@"<b>Special:</b> {specialString}
<b>US State:</b> {usState}
<b>Format:</b> 2015 onwards (Motorcycles)";
                } else if(plateReturn.Format == Enums.UsOhPlateFormat.duiOffender) {
                    output += $@"<b>US State:</b> {usState}
<b>Format:</b> DUI Offender

<i>This plate belongs to a DUI offender with limited driving privileges. Sucks to be them.</i>";
                }
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported Ohio plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "us-oh";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }

        private static ParsedPlateMessageReturnModel ParseUsScPlate(string plate)
        {
            ParsedPlateMessageReturnModel parsedPlateReturn = new ParsedPlateMessageReturnModel { };

            UsScPlateReturnModel plateReturn = UsScPlateUtilities.ParseUsScPlate(plate);

            parsedPlateReturn.Flag = "🇺🇸";
        
            string output = $@"#️⃣ <i>Parse Plate:</i> {parsedPlateReturn.Flag} <code>{plate}</code>
—
";

            string usState = "South Carolina";

            if(plateReturn.Valid) {
                if(plateReturn.Format == Enums.UsScPlateFormat.yr2008) {
                    output += $@"<b>US State:</b> {usState}
<b>Format:</b> 2008 onwards";
                }
            } else {
                output += "<i>This is an invalid, custom/private, or unsupported South Carolina plate. Contact</i> @theducky <i>if you believe it is a standard format.</i>";
            }

            parsedPlateReturn.CountryCode = "us-sc";
            parsedPlateReturn.FoundMatch = plateReturn.Valid;
            parsedPlateReturn.Message = output;

            return parsedPlateReturn;
        }
    }
}