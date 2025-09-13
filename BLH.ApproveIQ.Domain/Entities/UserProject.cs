using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Entities;

public class UserProject : AuditableEntity
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectRole { get; set; } = string.Empty;

    // Navigation properties
    public User User { get; set; } = null!;
    public Project Project { get; set; } = null!;
}
