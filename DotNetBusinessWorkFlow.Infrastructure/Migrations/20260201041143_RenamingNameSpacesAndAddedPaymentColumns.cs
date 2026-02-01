using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetBusinessWorkFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamingNameSpacesAndAddedPaymentColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Payments",
                newName: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Payments",
                newName: "InvoiceId");
        }
    }
}
