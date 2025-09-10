namespace BLH.ApproveIQ.Application.DTOs;

public class AuditLogDto
{
    public Guid? Id { get; set; }
    public string? Action { get; set; }
    public Guid EntityId { get; set; }
    public string? EntityName { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? AffectedColumns { get; set; }
    public Guid CreatedUserId { get; set; }
    public string? CreatedUserName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}