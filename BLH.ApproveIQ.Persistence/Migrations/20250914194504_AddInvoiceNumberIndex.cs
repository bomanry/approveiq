using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLH.ApproveIQ.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceNumberIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                table: "Invoices",
                column: "InvoiceNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoices_InvoiceNumber",
                table: "Invoices");
        }
    }
}
