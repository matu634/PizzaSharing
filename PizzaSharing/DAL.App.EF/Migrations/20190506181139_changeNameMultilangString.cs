using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class changeNameMultilangString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeName",
                table: "Changes");

            migrationBuilder.AddColumn<int>(
                name: "ChangeNameId",
                table: "Changes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Changes_ChangeNameId",
                table: "Changes",
                column: "ChangeNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_MultiLangStrings_ChangeNameId",
                table: "Changes",
                column: "ChangeNameId",
                principalTable: "MultiLangStrings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changes_MultiLangStrings_ChangeNameId",
                table: "Changes");

            migrationBuilder.DropIndex(
                name: "IX_Changes_ChangeNameId",
                table: "Changes");

            migrationBuilder.DropColumn(
                name: "ChangeNameId",
                table: "Changes");

            migrationBuilder.AddColumn<string>(
                name: "ChangeName",
                table: "Changes",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
