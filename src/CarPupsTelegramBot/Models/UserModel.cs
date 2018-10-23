using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarPupsTelegramBot.Models {
    public class UserModel {
        [Key]
        public long TelegramId { get; set; }
        
        public string TelegramUsername { get; set; }
        public string TelegramName { get; set; }
    }
}