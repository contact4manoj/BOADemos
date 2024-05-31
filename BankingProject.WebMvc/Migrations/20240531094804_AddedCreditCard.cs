
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingProject.WebMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreditCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditCardNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCardNumber",
                table: "AspNetUsers");
        }
    }
}
