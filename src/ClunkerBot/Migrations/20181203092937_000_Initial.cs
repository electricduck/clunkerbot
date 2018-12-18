using Microsoft.EntityFrameworkCore.Migrations;

namespace ClunkerBot.Migrations
{
    public partial class _000_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Garage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Plate = table.Column<string>(nullable: true),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Generation = table.Column<string>(nullable: true),
                    Trim = table.Column<string>(nullable: true),
                    EngineSize = table.Column<decimal>(nullable: false),
                    EngineType = table.Column<int>(nullable: true),
                    EngineCode = table.Column<string>(nullable: true),
                    EngineFuel = table.Column<int>(nullable: true),
                    Transmission = table.Column<int>(nullable: true),
                    DriveType = table.Column<int>(nullable: true),
                    FuellyId = table.Column<int>(nullable: false),
                    MainImage = table.Column<string>(nullable: true),
                    TelegramUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    TelegramId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TelegramUsername = table.Column<string>(nullable: true),
                    TelegramName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.TelegramId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Garage");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
