using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetBusinessWorkFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureOrderMoneyColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceAmount",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "Orders");
        }
    }
}
