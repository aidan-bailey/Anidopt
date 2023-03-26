using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anidopt.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptorType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DescriptorType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptorType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organisation_Name",
                table: "Organisation",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Breed_Name",
                table: "Breed",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DescriptorType");

            migrationBuilder.DropIndex(
                name: "IX_Organisation_Name",
                table: "Organisation");

            migrationBuilder.DropIndex(
                name: "IX_Breed_Name",
                table: "Breed");
        }
    }
}
