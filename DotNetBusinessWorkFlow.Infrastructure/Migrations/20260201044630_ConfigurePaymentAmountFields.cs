using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetBusinessWorkFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurePaymentAmountFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceAmount",
                table: "Payments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAmount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "Payments");
        }
    }
}
