using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLH.ApproveIQ.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPdfFileNameToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfFileName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            // Update existing invoices with their corresponding PDF filenames
            migrationBuilder.Sql(@"
                UPDATE Invoices SET PdfFileName = 'invoice_2001_BLH_final.pdf' 
                WHERE Id = '5505ee83-ead8-4275-8747-9f1dff39bc8c'");

            migrationBuilder.Sql(@"
                UPDATE Invoices SET PdfFileName = 'invoice_2002_BLH_final.pdf' 
                WHERE Id = '4d49a7c0-5a9f-4f72-85a3-7b97bd965a5e'");

            migrationBuilder.Sql(@"
                UPDATE Invoices SET PdfFileName = 'invoice_2003_BLH_final.pdf' 
                WHERE Id = 'f84e950d-a0ff-4b90-b970-c209b293cbb7'");

            migrationBuilder.Sql(@"
                UPDATE Invoices SET PdfFileName = 'invoice_2004_BLH_final.pdf' 
                WHERE Id = 'e01cc6c1-c8d2-444e-a5a0-c556fa7d44fe'");

            migrationBuilder.Sql(@"
                UPDATE Invoices SET PdfFileName = 'invoice_2005_BLH_final.pdf' 
                WHERE Id = 'bbdaba01-aa4e-42df-9e73-ce54d40e720b'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfFileName",
                table: "Invoices");
        }
    }
}
