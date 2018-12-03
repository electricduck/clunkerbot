using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CarPupsTelegramBot.Data;

namespace CarPupsTelegramBot.Models {
    public class GarageModel {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}

        public string Plate { get; set; }
        
        public string Make { get; set; }                            // e.g. Peugeot, Suzuki, Volkswagen, Honda, Citroen, Ford
        public string Model { get; set; }                           // e.g. 106, Carry1.3, Polo, Legend, C1, Escort
        public string Generation { get; set; }                      // e.g. Ph2, DA32, 9N3, KA9, AB10, Mk6 (EU)
        public string Trim { get; set; }
        public decimal EngineSize { get; set; }                     // e.g. 1.1, 1.3, 1.2. 3,5, 0.99, 1.8
        public Enums.EngineTypeEnum? EngineType { get; set; }       // e.g. I4, I3, I4, V6, I3, I4
        public string EngineCode { get; set; }                      // e.g. TU1JP/HFX, G13B, ?, ?, 1KR-FE, Zeta
        public Enums.EngineFuelEnum? EngineFuel { get; set; }       // e.g. Petrol, Petrol, Petrol, Petrol, Petrol, Petrol
        public Enums.TransmissionEnum? Transmission { get; set; }   // e.g. Manual, Manual, Manual, Manual, Manual, Manual
        public Enums.DriveTypeEnum? DriveType { get; set; }         // e.g. FWD, FWD, FWD, ?, FWD, FWD

        public int FuellyId { get; set; }

        public string MainImage { get; set; }

        public long TelegramUserId { get; set; }
    }
}