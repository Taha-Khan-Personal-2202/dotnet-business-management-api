using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetBusinessWorkFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureOrderItemMoneyColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceAmount",
                table: "OrderItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                table: "OrderItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAmount",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "OrderItems");
        }
    }
}
