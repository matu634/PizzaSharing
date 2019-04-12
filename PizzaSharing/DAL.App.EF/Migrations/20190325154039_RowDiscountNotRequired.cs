using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class RowDiscountNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RowDiscount",
                table: "ReceiptRows",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RowDiscount",
                table: "ReceiptRows",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
