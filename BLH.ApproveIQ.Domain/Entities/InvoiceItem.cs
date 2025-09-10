using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Entities;

public class InvoiceItem : AuditableEntity
{
    // Foreign Key to Invoice
    public Guid InvoiceId { get; set; }

    // Columns as shown in the "Invoice Line" table
    public string? BusinessUnit { get; set; }            // "Business Unit"
    public string? BuDescription { get; set; }           // "BU Description"
    public string? CostCode { get; set; }                // "Cost Code"
    public string? CostCodeDesc { get; set; }            // "Cost Code Desc"
    public string? CostType { get; set; }                // "Cost Type"
    public string? CostTypeDesc { get; set; }            // "Cost Type Desc"
    public string? OrderNumber { get; set; }             // "Order Number"
    public string? Line { get; set; }                    // "Line"
    public string? OrderSuffix { get; set; }             // "Order Suf..."
    public string? GlLineType { get; set; }              // "GL Line Type"
    public decimal? Qty { get; set; }                    // "QTY"
    public string? Uom { get; set; }                     // "UOM"
    public decimal? UnitPrice { get; set; }              // "Unit Price"
    public decimal? Amount { get; set; }                 // "Amount"

    // Navigation Property
    public Invoice Invoice { get; set; } = null!;
}