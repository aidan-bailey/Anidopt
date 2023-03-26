using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anidopt.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailInfoLinkRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoLink_Detail_DetailId",
                table: "InfoLink");

            migrationBuilder.DropIndex(
                name: "IX_InfoLink_DetailId",
                table: "InfoLink");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "InfoLink");

            migrationBuilder.AddColumn<int>(
                name: "InfoLinkId",
                table: "Detail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Detail_InfoLinkId",
                table: "Detail",
                column: "InfoLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detail_InfoLink_InfoLinkId",
                table: "Detail",
                column: "InfoLinkId",
                principalTable: "InfoLink",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detail_InfoLink_InfoLinkId",
                table: "Detail");

            migrationBuilder.DropIndex(
                name: "IX_Detail_InfoLinkId",
                table: "Detail");

            migrationBuilder.DropColumn(
                name: "InfoLinkId",
                table: "Detail");

            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "InfoLink",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InfoLink_DetailId",
                table: "InfoLink",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoLink_Detail_DetailId",
                table: "InfoLink",
                column: "DetailId",
                principalTable: "Detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
