using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models.ReturnModels;

namespace CarPupsTelegramBot.Commands
{
    class Fuelly
    {
        public static readonly string fuellyBaseUrl = "http://www.fuelly.com";
        public static readonly string fuellyCarProfileUrl = fuellyBaseUrl + "/car/a/a/0/a/";
        public static readonly string fuellyNoImage = fuellyBaseUrl + "/img/no-car-image-square.gif";

        public static ImageMessageReturnModel Get(string fuellyId) {
            var document = ScrapeFuellyProfilePage(fuellyId);

            var strongNodes = document.SelectNodes("//strong");
            var h4Nodes = document.SelectNodes("//h4");
            var profileLinkNodes = document.SelectNodes("//a[@class='profile-link']");
            var profilePhotoNodes = document.SelectNodes("//div[@class='profile-photo']//img");

            string extractedAvgMpg = strongNodes[2].InnerHtml;
            string extractedLastMpg = strongNodes[3].InnerHtml;
            string extractedBestMpg = strongNodes[4].InnerHtml;
            string extractedVehicleInfo = h4Nodes[0].InnerText.Replace("  ", " ").Replace("\t","").Replace("\n","").Replace("\r","").Trim();
            string extractedProfileName = profileLinkNodes[0].InnerText.Replace(" ","");
            string extractedProfileLink = profileLinkNodes[0].Attributes["href"].Value;
            string extractedProfilePhoto = fuellyNoImage;
            
            string pageLink = fuellyCarProfileUrl + fuellyId;

            if(profilePhotoNodes != null) {
                extractedProfilePhoto = fuellyBaseUrl + profilePhotoNodes[0].Attributes["src"].Value;
            }

            var caption = $@"MPG summary for <i>{extractedProfileName}'s</i> <b>{extractedVehicleInfo}</b>:

Average: <b>{extractedAvgMpg} MPG</b>
Best: <b>{extractedBestMpg} MPG</b>
Last: <b>{extractedLastMpg} MPG</b>

🔗 {pageLink}";

            ImageMessageReturnModel output = new ImageMessageReturnModel {
                Caption = caption,
                PhotoUrl = extractedProfilePhoto
            };

            return output;
        }

        //public static string My() {
        //
        //}

        //public static string Set() {
        //
        //}

        private static HtmlNode ScrapeFuellyProfilePage(string fuellyId) {
            var url = fuellyCarProfileUrl + fuellyId;
            var web = new HtmlWeb();
            var document = web.Load(url);

            return document.DocumentNode;
        }
    }
}