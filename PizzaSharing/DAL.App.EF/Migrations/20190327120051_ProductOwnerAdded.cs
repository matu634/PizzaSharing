using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class ProductOwnerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrganizationId",
                table: "Products",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Organizations_OrganizationId",
                table: "Products",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Organizations_OrganizationId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrganizationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Products");
        }
    }
}
