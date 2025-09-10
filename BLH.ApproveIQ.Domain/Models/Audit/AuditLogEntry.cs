using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using BLH.ApproveIQ.Domain.Constants;
using BLH.ApproveIQ.Domain.Entities;

namespace BLH.ApproveIQ.Domain.Models.Audit;
public class AuditLogEntry
{
    public AuditLogEntry(EntityEntry entry)
    {
        Entry = entry;
    }
    public EntityEntry Entry { get; }
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }
    public string? EntityName { get; set; }
    public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
    public AuditLogAction AuditLogAction { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();
    public AuditLog ToAudit()
    {
        var audit = new AuditLog();
        audit.CreatedUserId = UserId;
        audit.Action = AuditLogAction.ToString();
        audit.EntityId = EntityId;
        audit.EntityName = EntityName;
        audit.CreatedOnUtc = DateTime.UtcNow;
        audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
        audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
        audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
        return audit;
    }
}

