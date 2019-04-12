using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class ChangesOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Changes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Changes_OrganizationId",
                table: "Changes",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Changes_Organizations_OrganizationId",
                table: "Changes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Changes_Organizations_OrganizationId",
                table: "Changes");

            migrationBuilder.DropIndex(
                name: "IX_Changes_OrganizationId",
                table: "Changes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Changes");
        }
    }
}
