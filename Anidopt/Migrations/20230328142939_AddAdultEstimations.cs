using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anidopt.Migrations
{
    /// <inheritdoc />
    public partial class AddAdultEstimations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AdultHeightEstimation",
                table: "Animal",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AdultWeightEstimation",
                table: "Animal",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultHeightEstimation",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "AdultWeightEstimation",
                table: "Animal");
        }
    }
}
