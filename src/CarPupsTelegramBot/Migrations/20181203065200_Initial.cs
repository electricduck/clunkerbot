using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPupsTelegramBot.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Garage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Plate = table.Column<string>(nullable: true),
                    Make = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Generation = table.Column<string>(nullable: true),
                    EngineSize = table.Column<string>(nullable: true),
                    EngineType = table.Column<int>(nullable: true),
                    EngineCode = table.Column<string>(nullable: true),
                    EngineFuel = table.Column<int>(nullable: true),
                    Transmission = table.Column<int>(nullable: true),
                    DriveType = table.Column<int>(nullable: true),
                    FuellyId = table.Column<int>(nullable: false),
                    MainImage = table.Column<string>(nullable: true),
                    UserTelegramId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garage_Users_UserTelegramId",
                        column: x => x.UserTelegramId,
                        principalTable: "Users",
                        principalColumn: "TelegramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Garage_UserTelegramId",
                table: "Garage",
                column: "UserTelegramId");
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
