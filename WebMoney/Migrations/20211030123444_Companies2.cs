using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMoney.Migrations
{
    public partial class Companies2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Walletnumber",
                value: "MEGACOM11111");

            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Walletnumber",
                value: "AKNET2222222");

            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Walletnumber",
                value: "SVET33333333");

            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Walletnumber",
                value: "GAS444444444");

            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "Walletnumber",
                value: "O!5555555555");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Walletnumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Walletnumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Walletnumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Walletnumber",
                value: null);

            migrationBuilder.UpdateData(
                table: "CompanyUsers",
                keyColumn: "Id",
                keyValue: 5,
                column: "Walletnumber",
                value: null);
        }
    }
}
