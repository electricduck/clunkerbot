using Microsoft.EntityFrameworkCore;
using CarPupsTelegramBot.Models;

namespace CarPupsTelegramBot.Data {
    public class CarPupsTelegramBotContext : DbContext {
        public DbSet<FuellyModel> Fuelly { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=bot.db"); // TODO: Add to config
        }
    }
}