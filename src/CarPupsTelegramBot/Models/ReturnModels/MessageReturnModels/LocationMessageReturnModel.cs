using System;

namespace CarPupsTelegramBot.Models.ReturnModels.MessageReturnModels
{
    public class LocationMessageReturnModel
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int LivePeriod { get; set; }
    }
}