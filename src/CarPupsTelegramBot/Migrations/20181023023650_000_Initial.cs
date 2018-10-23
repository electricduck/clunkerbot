using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPupsTelegramBot.Migrations
{
    public partial class _000_Initial : Migration
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
                name: "Fuelly",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FuellyId = table.Column<int>(nullable: false),
                    UserTelegramId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuelly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fuelly_Users_UserTelegramId",
                        column: x => x.UserTelegramId,
                        principalTable: "Users",
                        principalColumn: "TelegramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fuelly_UserTelegramId",
                table: "Fuelly",
                column: "UserTelegramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fuelly");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
