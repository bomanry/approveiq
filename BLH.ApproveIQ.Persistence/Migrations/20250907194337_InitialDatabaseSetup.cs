using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLH.ApproveIQ.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    OldValues = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    NewValues = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "B2CUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPrincipalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountEnabled = table.Column<bool>(type: "bit", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "NoAccess"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B2CUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerExceptionStackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApInvoiceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LegacyOrderNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BuJobNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JdeOrderNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VendorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendorType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendorStreetAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VendorCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VendorState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendorZip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GlDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MiscAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FreightAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TaxableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxExCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TaxArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RetainagePct = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    RetainageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AmountToPay = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PaymentHoldFlag = table.Column<bool>(type: "bit", nullable: true),
                    InvoiceDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VoucherNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CheckNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessageConsumers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessageConsumers", x => new { x.Id, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BuDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CostCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostCodeDesc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CostType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostTypeDesc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Line = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OrderSuffix = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GlLineType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Qty = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Uom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "B2CUsers");

            migrationBuilder.DropTable(
                name: "ExceptionLogs");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "OutboxMessageConsumers");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
