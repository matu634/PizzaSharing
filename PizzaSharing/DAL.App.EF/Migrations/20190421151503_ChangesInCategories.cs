using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class ChangesInCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ChangeInCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    ChangeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeInCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeInCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeInCategories_Changes_ChangeId",
                        column: x => x.ChangeId,
                        principalTable: "Changes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeInCategories_CategoryId",
                table: "ChangeInCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeInCategories_ChangeId",
                table: "ChangeInCategories",
                column: "ChangeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeInCategories");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
