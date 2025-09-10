using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Entities;

public class B2CUser : AuditableEntity
{
    public string? DisplayName { get; set; }
    public string? UserPrincipalName { get; set; }
    public bool? AccountEnabled { get; set; }
    public string Role { get; set; }
}
