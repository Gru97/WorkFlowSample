using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WFE.Console.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "Owner" },
                values: new object[] { new Guid("020df25b-d01b-4a3b-9c2e-d73a31f2d1dd"), "9852333", 200m, "Taghi" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "Owner" },
                values: new object[] { new Guid("1a09b61b-bfd1-44f6-adb9-bf2e17c2e6f1"), "5245688", 200m, "Kokab" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "Owner" },
                values: new object[] { new Guid("e07ed267-a765-431c-a420-941fd5cf429f"), "1258823", 200m, "Gholi" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
