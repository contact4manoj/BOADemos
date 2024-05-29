using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingProject.WebMvc.Migrations
{
    /// <inheritdoc />
    public partial class Version03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CId",
                schema: "dbo",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CId",
                schema: "dbo",
                table: "Products",
                column: "CId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CId",
                schema: "dbo",
                table: "Products",
                column: "CId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CId",
                schema: "dbo",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CId",
                schema: "dbo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CId",
                schema: "dbo",
                table: "Products");
        }
    }
}
