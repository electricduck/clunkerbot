using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Models.ReturnModels;
using CarPupsTelegramBot.Models.ReturnModels.MessageReturnModels;

namespace CarPupsTelegramBot.Commands
{
    class Fuelly
    {
        public static readonly string fuellyBaseUrl = "http://www.fuelly.com";
        public static readonly string fuellyCarProfileUrl = fuellyBaseUrl + "/car/a/a/0/a/";
        public static readonly string fuellyNoImage = fuellyBaseUrl + "/img/no-car-image-square.gif";

        public static readonly double usMpgUkMpgRate = 1.2009499255398;

        public static ImageMessageReturnModel Get(string fuellyId, string unit = "us") {
            try {
                var document = ScrapeFuellyProfilePage(fuellyId);

                var strongNodes = document.SelectNodes("//strong");
                var h4Nodes = document.SelectNodes("//h4");
                var profileLinkNodes = document.SelectNodes("//a[@class='profile-link']");
                var profilePhotoNodes = document.SelectNodes("//div[@class='profile-photo']//img");
                var totalMilesDigitsNodes = document.SelectNodes("//ul[@class='total-miles-digits']//li");
                var extendedInfoNodes = document.SelectNodes("//p[@class='extended_desc']//small");
                var parentLinkNodes = document.SelectNodes("//ul[@class='breadcrumb']//li//a");

                double extractedAvgMpg = 0;
                double extractedLastMpg = 0;
                double extractedBestMpg = 0;
                int extractedMilesTracked = 0;

                string extractedAvgMpgString = strongNodes[2].InnerHtml;
                string extractedLastMpgString = strongNodes[3].InnerHtml;
                string extractedBestMpgString = strongNodes[4].InnerHtml;
                string extractedVehicleInfo = h4Nodes[0].InnerText.Replace("  ", " ").Replace("\t","").Replace("\n","").Replace("\r","").Trim();
                string extractedProfileName = profileLinkNodes[0].InnerText.Trim();
                string extractedProfileLink = profileLinkNodes[0].Attributes["href"].Value;
                string extractedProfilePhoto = fuellyNoImage;
                string extractedExtendedVehicleInfo = extendedInfoNodes[0].InnerText;
                string extractedParentLink = parentLinkNodes[4].Attributes["href"].Value;

                string pageLink = extractedParentLink + "/" + extractedProfileName.ToLower() + "/" + fuellyId;

                if(profilePhotoNodes != null) {
                    extractedProfilePhoto = fuellyBaseUrl + profilePhotoNodes[0].Attributes["src"].Value;
                }

                if(totalMilesDigitsNodes != null) {
                    string extractedMilesTrackedString = "";

                    foreach(var digit in totalMilesDigitsNodes) {
                        extractedMilesTrackedString += digit.InnerText;
                    }

                    extractedMilesTracked = Convert.ToInt32(extractedMilesTrackedString);
                }

                FuellyMpgUnitsReturnModel convertedUnits = ConvertFuellyMpgUnits(
                    extractedAvgMpgString,
                    extractedBestMpgString,
                    extractedLastMpgString,
                    unit
                );

                extractedAvgMpg = convertedUnits.Average;
                extractedBestMpg = convertedUnits.Best;
                extractedLastMpg = convertedUnits.Last;
                
                string presentedUnit = convertedUnits.PresentedUnit;

                var caption = $@"⛽ <i>Get Fuelly:</i> <code>{fuellyId}</code>
—      
MPG summary for a <b>{extractedVehicleInfo}</b> <i>({extractedExtendedVehicleInfo})</i>, owned by <b>{extractedProfileName}</b>, with <b>{extractedMilesTracked.ToString()} Miles</b> tracked:

<b>Average:</b> {extractedAvgMpg} {presentedUnit}
<b>Best:</b> {extractedBestMpg} {presentedUnit}
<b>Last:</b> {extractedLastMpg} {presentedUnit}

🔗 {pageLink}";

                ImageMessageReturnModel output = new ImageMessageReturnModel {
                    Caption = caption,
                    PhotoUrl = extractedProfilePhoto
                };

                return output;
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
                return null;
            }
        }

        //public static string My() {
        //
        //}

        public static string Set(string fuellyId) {
            int telegramUserId = 63391517;

            UserModel userItem = new UserModel {
                TelegramId = telegramUserId
            };

            FuellyModel fuellyItem = new FuellyModel {
                FuellyId = Convert.ToInt32(fuellyId),
                User = userItem
            };

            using (var db = new CarPupsTelegramBotContext()) {
                db.Fuelly.Add(fuellyItem);
                var dbCount = db.SaveChanges();

                Console.WriteLine($"🗄️ {dbCount} records saved to database");
            }

            return "derp";
        }

        private static FuellyMpgUnitsReturnModel ConvertFuellyMpgUnits(string averageMpg, string bestMpg, string lastMpg, string unit) {
            double convertedAverageMpg = Convert.ToDouble(averageMpg);
            double convertedBestMpg = Convert.ToDouble(bestMpg);
            double convertedLastMpg = Convert.ToDouble(lastMpg);

            double conversionRate = 0;

            string presentedUnit;

            switch(unit.ToLower()) {
                case "uk":
                    conversionRate = usMpgUkMpgRate;
                    presentedUnit = "UK MPG";
                    break;
                default:
                    conversionRate = 1;
                    presentedUnit = "MPG";
                    break;
            };

            FuellyMpgUnitsReturnModel output = new FuellyMpgUnitsReturnModel {
                Average = Math.Round(convertedAverageMpg * conversionRate, 2, MidpointRounding.ToEven),
                Best = Math.Round(convertedBestMpg * conversionRate, 2, MidpointRounding.ToEven),
                Last = Math.Round(convertedLastMpg * conversionRate, 2, MidpointRounding.ToEven),
                PresentedUnit = presentedUnit
            };

            return output;
        }

        private static HtmlNode ScrapeFuellyProfilePage(string fuellyId) {
            var url = fuellyCarProfileUrl + fuellyId;
            var web = new HtmlWeb();
            var document = web.Load(url);

            return document.DocumentNode;
        }
    }
}