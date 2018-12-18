using Microsoft.EntityFrameworkCore;
using ClunkerBot.Models;

namespace ClunkerBot.Data {
    public class ClunkerBotContext : DbContext {
        public DbSet<GarageModel> Garage { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=bot.db"); // TODO: Add to config
        }
    }
}