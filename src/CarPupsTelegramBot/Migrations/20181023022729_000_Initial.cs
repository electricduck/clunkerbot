using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPupsTelegramBot.Migrations
{
    public partial class _000_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fuelly",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FuellyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuelly", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fuelly");
        }
    }
}
