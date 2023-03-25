using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anidopt.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalTypeToBreed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalTypeId",
                table: "Breed",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Breed_AnimalTypeId",
                table: "Breed",
                column: "AnimalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breed_AnimalType_AnimalTypeId",
                table: "Breed",
                column: "AnimalTypeId",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breed_AnimalType_AnimalTypeId",
                table: "Breed");

            migrationBuilder.DropIndex(
                name: "IX_Breed_AnimalTypeId",
                table: "Breed");

            migrationBuilder.DropColumn(
                name: "AnimalTypeId",
                table: "Breed");
        }
    }
}
