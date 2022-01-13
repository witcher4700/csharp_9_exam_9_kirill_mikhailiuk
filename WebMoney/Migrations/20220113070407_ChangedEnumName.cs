using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMoney.Migrations
{
    public partial class ChangedEnumName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransActionType",
                table: "Transactions",
                newName: "TransactionType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionType",
                table: "Transactions",
                newName: "TransActionType");
        }
    }
}
