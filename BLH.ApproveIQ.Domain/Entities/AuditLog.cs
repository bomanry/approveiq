using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Entities;
public class AuditLog : Entity
{
    public string? Action { get; set; }
    public Guid EntityId { get; set; }
    public string? EntityName { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? AffectedColumns { get; set; }
    public Guid CreatedUserId { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}

