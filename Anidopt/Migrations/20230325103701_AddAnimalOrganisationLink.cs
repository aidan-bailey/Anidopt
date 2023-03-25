using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anidopt.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalOrganisationLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_OrganisationId",
                table: "Animal",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Organisation_OrganisationId",
                table: "Animal",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Organisation_OrganisationId",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_OrganisationId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Animal");
        }
    }
}
