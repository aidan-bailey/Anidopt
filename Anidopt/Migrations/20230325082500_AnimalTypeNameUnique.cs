using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anidopt.Migrations
{
    /// <inheritdoc />
    public partial class AnimalTypeNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AnimalType_Name",
                table: "AnimalType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnimalType_Name",
                table: "AnimalType");
        }
    }
}
