using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudCityCakesMVC.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CakeOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topping = table.Column<string>(maxLength: 64, nullable: false),
                    Frosting = table.Column<string>(maxLength: 64, nullable: false),
                    Flavour = table.Column<string>(maxLength: 128, nullable: false),
                    Size = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal", nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    CompletedDate = table.Column<DateTime>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CakeOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CakeOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CakeOrders_UserId",
                table: "CakeOrders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CakeOrders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
