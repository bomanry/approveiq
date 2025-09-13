using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Entities;

public class InvoiceApproval : AuditableEntity
{
    public Guid InvoiceId { get; set; }
    public Guid FromUserId { get; set; }        // Who assigned/reassigned (could be system for initial)
    public Guid ToUserId { get; set; }          // Who it was assigned to
    public string FromStatus { get; set; } = string.Empty;  // Previous status
    public string ToStatus { get; set; } = string.Empty;    // New status
    public string? Comments { get; set; }       // Optional comments about the action
    public string Action { get; set; } = string.Empty;      // "Assigned", "Approved", "Rejected", "Reassigned"

    // Navigation properties
    public Invoice Invoice { get; set; } = null!;
    public User FromUser { get; set; } = null!;
    public User ToUser { get; set; } = null!;
}
