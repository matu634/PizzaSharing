using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangeCategoryPropertryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Changes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Changes_CategoryId",
                table: "Changes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_Categories_CategoryId",
                table: "Changes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changes_Categories_CategoryId",
                table: "Changes");

            migrationBuilder.DropIndex(
                name: "IX_Changes_CategoryId",
                table: "Changes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Changes");
        }
    }
}
