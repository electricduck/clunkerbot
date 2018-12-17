using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels.PlateReturnModels;

// TODO: Include all the overseas territories
// SEE: https://en.wikipedia.org/wiki/Vehicle_registration_plates_of_France

namespace CarPupsTelegramBot.Utilities.PlateUtilities
{
    public class FrPlateUtilities
    {
        private static string Year1950Regex = @"^(([0-9]{1,4})([A-Z]{1,3})([0-9]{1,2}))$";
        private static string Year2009Regex = @"^(([A-Z]{2})-([0-9]{3})-([A-Z]{2}))$";
    
        public static FrPlateReturnModel ParseFrPlate(string plate)
        {
            FrPlateReturnModel plateReturn = null;

            plate.Replace(" ", "");

            if(Regex.IsMatch(plate, Year1950Regex)) {
                plateReturn = ParseFrYr1950Plate(plate);
            } else if(Regex.IsMatch(plate, Year2009Regex)) {
                plateReturn = ParseFrYr2009Plate(plate);
            } else {
                plateReturn = new FrPlateReturnModel {
                    Valid = false
                };
            }

            return plateReturn;
        }

        // TODO: Include special plates
        private static FrPlateReturnModel ParseFrYr1950Plate(string plate)
        {
            Regex regex = new Regex(Year1950Regex);
            Match match = regex.Match(plate);

            FrPlateReturnModel returnModel = new FrPlateReturnModel {
                Format = Enums.FrPlateFormat.yr1950,
                Valid = true
            };

            returnModel.Location = GetYr1950FrLocationCode(Convert.ToInt32(match.Groups[4].Value));

            return returnModel;
        }

        private static FrPlateReturnModel ParseFrYr2009Plate(string plate)
        {
            Regex regex = new Regex(Year2009Regex);
            Match match = regex.Match(plate);

            FrPlateReturnModel returnModel = new FrPlateReturnModel {
                Format = Enums.FrPlateFormat.yr2009,
                Valid = true
            };

            int issue = GetYr2009Issue(
                match.Groups[2].Value,
                match.Groups[4].Value,
                Convert.ToInt32(match.Groups[3].Value)
            );

            returnModel.Issue = issue.ToString();
            returnModel.Year = GuessYr2009Year(issue).ToString();

            return returnModel;
        }

        private static int GuessYr2009Year(int issue)
        {
            int approxLifespanOfSIV = 80;
            int maxIssue = GetYr2009Issue("ZZ", "ZZ", 999);
            int averageRegistrationsPerYear = maxIssue/approxLifespanOfSIV;
            int year = 2003;
            int yearModifier = 0;

            do
            {
                yearModifier++;
                year++;
            } while(!(issue < averageRegistrationsPerYear*yearModifier));

            return year;
        }

        // TODO: Ignore I, O, and U letters
        private static int GetYr2009Issue(string firstLetters, string lastLetters, int number)
        {
            int result = 0;

            string letters = $@"{firstLetters}{lastLetters}";

            // for (int i = 0; i < letters.Length; i++)
            // {
            //     result *= 26;
            //     //result *= 23;
            //     char letter = letters[i];

            //     if (letter < 'A') letter = 'A';
            //     if (letter > 'Z') letter = 'Z';

            //     int letterCode = GetYr2009FrIssueCodeLocation(letter);

            //     int maxLetter = GetYr2009FrIssueCodeLocation('Z');

            //     result += (int)letter - (int)'A' + 1;
            //     //result += letterCode - (int)'A' + 1;
            // }

            //int maxLetter = GetYr2009FrIssueCodeLocation('Z');

            //result = (result-18278)*999+number-999;

            int firstLetter = GetYr2009FrIssueCodeLocation(letters[3]);
            int secondLetter = GetYr2009FrIssueCodeLocation(letters[2]);
            int thirdLetter = GetYr2009FrIssueCodeLocation(letters[1]);
            int fourthLetter = GetYr2009FrIssueCodeLocation(letters[0]);

            //int letterTotal = (22*12167)+(22*529)+(22*23)+22

            int letterTotal = (fourthLetter * 22) +
                (thirdLetter * 529) +
                (secondLetter * 23) +
                firstLetter
                + 1;

            int plateTotal = ((fourthLetter * 22) +
                (thirdLetter * 529) +
                (letterTotal * 999) +
                (secondLetter * 23) +
                firstLetter);

            return plateTotal;
        }

        private static int GetYr2009FrIssueCodeLocation(char issueCode)
        {
            if(Yr2009FrIssueCodeLocations.ContainsKey(issueCode)) {
                int issue;

                Yr2009FrIssueCodeLocations.TryGetValue(issueCode, out issue);

                return issue;
            } else {
                return 0;
            }
        }


        private static string GetYr1950FrLocationCode(int locationCode)
        {
            if(Yr1950FrLocationCodes.ContainsKey(locationCode)) {
                string location;

                Yr1950FrLocationCodes.TryGetValue(locationCode, out location);

                return location;
            } else {
                return "";
            }
        }

        private static Dictionary<char, int> Yr2009FrIssueCodeLocations = new Dictionary<char, int>()
        {
            {'A', 0},
            {'B', 1},
            {'C', 2},
            {'D', 3},
            {'E', 4},
            {'F', 5},
            {'G', 6},
            {'H', 7},
            {'J', 8},
            {'K', 9},
            {'L', 10},
            {'M', 11},
            {'N', 12},
            {'P', 13},
            {'Q', 14},
            {'R', 15},
            {'S', 16},
            {'T', 17},
            {'V', 18},
            {'W', 19},
            {'X', 20},
            {'Y', 21},
            {'Z', 22}
        };

        private static Dictionary<int, string> Yr1950FrLocationCodes = new Dictionary<int, string>()
        {
            {01, "Ain, Bourg-en-Bresse"},
            {02, "Aisne, Laon"},
            {03, "Allier, Moulins"},
            {04, "Alpes-de-Haute-Provence, Digne-les-Bains"},
            {05, "Hautes-Alpes, Gap"},
            {06, "Alpes-Maritimes, Nice"},
            {07, "Ardèche, Privas"},
            {08, "Ardenes, Charleville-Mézières"},
            {09, "Ariège, Foix"},
            {10, "Aube, Troyes"},
            {11, "Aude, Carcassone"},
            {12, "Aveyron, Rodez"},
            {13, "Bouches-du-Rhône, Marseille"},
            {14, "Calvados, Caen"},
            {15, "Cantal, Aurillac"},
            {16, "Charente, Angoulême"},
            {17, "Charente-Maritime, La Rochelle"},
            {18, "Cher, Bourges"},
            {19, "Corrèze, Tulle"},
            {20, "Corse, Ajaccio"},
            {21, "Côte-d'Or, Dijon"},
            {22, "Côtes-d'Armor, Saint-Brieuc"},
            {23, "Creuse, Guéret"},
            {24, "Dordogne, Périgueux"},
            {25, "Doubs, Besançon"},
            {26, "Drôme, Valence"},
            {27, "Eure, Évreux"},
            {28, "Eure-et-Loir, Chartres"},
            {29, "Finistère, Quimper"},
            {30, "Gard, Nîmes"},
            {31, "Haute-Garonne, Toulouse"},
            {32, "Gers, Auch"},
            {33, "Gironde, Bordeaux"},
            {34, "Hérault, Montpellier"},
            {35, "Ille-et-Vilaine, Rennes"},
            {36, "Indre, Châteauroux"},
            {37, "Indre-et-Loire, Tours"},
            {38, "Isère, Grenoble"},
            {39, "Jura, Lons-le-Saunier"},
            {40, "Landes, Mont-de-Marsan"},
            {41, "Loir-et-Cher, Blois"},
            {42, "Loire, Saint-Étienne"},
            {43, "Haute-Loire, Le Puy-en-Velay"},
            {44, "Loire-Atlantique, Nantes"},
            {45, "Loiret, Orléans"},
            {46, "Lot, Cahros"},
            {47, "Lot-et-Garonne, Agen"},
            {48, "Lozère, Mende"},
            {49, "Maine-et-Loire, Angers"},
            {50, "Manche, Saint-Lô"},
            {51, "Marne, Châlons-en-Champagne"},
            {52, "Haute-Marne, Chaumont"},
            {53, "Mayenne, Laval"},
            {54, "Meurthe-et-Moselle, Nancy"},
            {55, "Meuse, Bar-le-DEc"},
            {56, "Morbihan, Vannes"},
            {57, "Moselle, Metz"},
            {58, "Nièvre, Nevers"},
            {59, "Nord, Lille"},
            {60, "Oise, Beauvais"},
            {61, "Orne, Alençon"},
            {62, "Pas-de-Calais, Arras"},
            {63, "Puy-de-Dôme, Clermont-Ferrand"},
            {64, "Pyrénées-Atlantiques, Pau"},
            {65, "Hautes-Pyrénées, Tarbes"},
            {66, "Pyrénées-Orientales, Perpignan"},
            {67, "Bas-Rhin, Strasbourg"},
            {68, "Haut-Rhin, Colmar"},
            {69, "Rhône, Lyon"},
            {70, "Haute-Saône, Vesoul"},
            {71, "Saône-et-Loire, Mâcon"},
            {72, "Sarthe, Le Mans"},
            {73, "Savoie, Chambéry"},
            {74, "Haute-Savoie, Annecy"},
            {75, "Paris, Paris"},
            {76, "Seine-Maritime, Rouen"},
            {77, "Seine-et-Marne, Melin"},
            {78, "Yvelines, Versailles"},
            {79, "Deux-Sèvres, Niort"},
            {80, "Somme, Amiens"},
            {81, "Tarn, Albi"},
            {82, "Tarn-et-Garonne, Montauban"},
            {83, "Var, Toulon"},
            {84, "Vaucluse, Avignon"},
            {85, "Vendée, La Roche-sur-Yon"},
            {86, "Vienne, Poitiers"},
            {87, "Haute-Vienne, Limoges"},
            {88, "Vosges, Épinal"},
            {89, "Yonne, Auxerre"},
            {90, "Territoire de Belfort, Belfort"},
            {91, "Essonne, Évry"},
            {92, "Hauts-de-Seine, Nanterre"},
            {93, "Seine-Saint-Denis, Bobigny"},
            {94, "Val-de-Marne, Créteil"},
            {95, "Val-d'Oise, Pontoise"}
        };
    }
}