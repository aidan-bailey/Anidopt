using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anidopt.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalTypeToAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalTypeId",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_AnimalTypeId",
                table: "Animal",
                column: "AnimalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AnimalType_AnimalTypeId",
                table: "Animal",
                column: "AnimalTypeId",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AnimalType_AnimalTypeId",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_AnimalTypeId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "AnimalTypeId",
                table: "Animal");
        }
    }
}
