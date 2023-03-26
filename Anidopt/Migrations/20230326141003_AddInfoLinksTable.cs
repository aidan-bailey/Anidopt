using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anidopt.Migrations
{
    /// <inheritdoc />
    public partial class AddInfoLinksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    DescriptorId = table.Column<int>(type: "int", nullable: false),
                    DetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoLink_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoLink_Descriptor_DescriptorId",
                        column: x => x.DescriptorId,
                        principalTable: "Descriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoLink_Detail_DetailId",
                        column: x => x.DetailId,
                        principalTable: "Detail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoLink_AnimalId",
                table: "InfoLink",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoLink_DescriptorId",
                table: "InfoLink",
                column: "DescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoLink_DetailId",
                table: "InfoLink",
                column: "DetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoLink");
        }
    }
}
