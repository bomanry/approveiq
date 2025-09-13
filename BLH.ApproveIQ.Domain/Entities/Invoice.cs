using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Entities;

public class Invoice : AuditableEntity
{
    // --- Invoice Info (top bar) ---
    public string? ApInvoiceType { get; set; }           // "AP Invoice Type"
    public string? InvoiceStatus { get; set; }           // "Invoice Status"
    public string? LegacyOrderNumber { get; set; }       // "Legacy Order Number"
    public string? BuJobNumber { get; set; }             // "BU / Job Number"
    public string? OrderType { get; set; }               // "Order Type"
    public string? Category { get; set; }                // "Category"

    // --- Project Relationship ---
    public Guid? ProjectId { get; set; }                 // Foreign key to Project
    public Project? Project { get; set; }                // Navigation property

    // --- Current Assignment ---
    public Guid? CurrentlyAssignedToUserId { get; set; } // Who is responsible now
    public User? CurrentlyAssignedToUser { get; set; }   // Navigation property

    // Company / JDE (left top area)
    public string? Company { get; set; }                 // "Company"
    public string? JdeOrderNumber { get; set; }          // "JDE Order Number"

    // --- Vendor Info ---
    public string? VendorId { get; set; }                // "Vendor ID"
    public string? VendorName { get; set; }              // "Name"
    public string? VendorType { get; set; }              // "Vendor Type"
    public string? VendorStreetAddress { get; set; }     // "Street Address"
    public string? VendorCity { get; set; }              // "City"
    public string? VendorState { get; set; }             // "State"
    public string? VendorZip { get; set; }               // "Zip"

    // --- Date Info ---
    public DateTime? GlDate { get; set; }                // "GL Date"
    public DateTime? InvoiceDate { get; set; }           // "Invoice Date"
    public DateTime? ReceivedDate { get; set; }          // "Received Date"

    // --- Amount Info (center band) ---
    public decimal? NetAmount { get; set; }              // "Net Amount"
    public decimal? MiscAmount { get; set; }             // "Misc Amount"
    public decimal? FreightAmount { get; set; }          // "Freight Amount"
    public decimal? GrossAmount { get; set; }            // "Gross Amount"
    public string? PaymentTerms { get; set; }            // "Payment Terms"

    public decimal? TaxableAmount { get; set; }          // "Taxable Amount"
    public string? TaxExCode { get; set; }               // "Tax EX" (code/flag shown in UI)
    public string? TaxArea { get; set; }                 // "Tax Area"
    public decimal? TaxAmount { get; set; }              // "Tax Amount"

    public decimal? RetainagePct { get; set; }           // "Retainage Pct"
    public decimal? RetainageAmount { get; set; }        // "Retainage Amount"
    public decimal? AmountToPay { get; set; }            // "Amount to Pay"
    public string? Currency { get; set; }                // "Currency"
    public bool? PaymentHoldFlag { get; set; }           // "Payment Hold Flag" (Y/N)

    // --- Processing Info (bottom band) ---
    public string? InvoiceDescription { get; set; }      // "Invoice Description"
    public string? VoucherNumber { get; set; }           // "Voucher Number"
    public string? CheckNumber { get; set; }             // "Check Number"
    public DateTime? CheckDate { get; set; }             // "Check Date"

    // --- Navigation Properties ---
    public List<InvoiceItem> Lines { get; set; } = new();
}