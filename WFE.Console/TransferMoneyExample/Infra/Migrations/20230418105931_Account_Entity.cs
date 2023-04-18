using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WFE.Console.TransferMoneyExample.Infra.Migrations
{
    public partial class Account_Entity : Migration
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
                values: new object[] { new Guid("1179372c-1ce0-4cc5-a2f3-35b0c69ae035"), "1258823", 200m, "Gholi" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "Owner" },
                values: new object[] { new Guid("5dbf28b7-5a55-48af-9682-240ceb0a090d"), "5245688", 200m, "Kokab" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "Owner" },
                values: new object[] { new Guid("8629049e-5bbc-4ac0-ba78-864baafe1f00"), "9852333", 200m, "Taghi" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
