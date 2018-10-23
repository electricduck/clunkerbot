using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarPupsTelegramBot.Models {
    public class GarageModel {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set;}

        public string Plate { get; set; }

        public string Brand { get; set; }                                       // e.g. Peugeot 106, Suzuki Carry 1.3, Volkswagen Polo, Honda Legend
        public string Code { get; set; }                                        // e.g. II, DA32, 9N3, KA9
        public double Mark { get; set; }                                        // e.g. 2.0, 0.0, 4.1, 3.0
        public double EngineSize { get; set; }                                  // e.g. 1.1, 1.3, 1.2, 3.5
        public Enums.EngineTypeEnum? EngineType { get; set; }                   // e.g. I4, I3, I4, V6
        public string EngineCode { get; set; }                                  // e.g. TU1JP/HFX, G13B, [NULL], [NULL]
        public Enums.EngineFuelEnum? EngineFuel { get; set; }                   // e.g. Petrol, Petrol, Petrol, Petrol
       
        public int? FuellyId { get; set; }

        public UserModel User { get; set; }
    }
}