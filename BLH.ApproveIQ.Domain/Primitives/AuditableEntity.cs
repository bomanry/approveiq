
namespace BLH.ApproveIQ.Domain.Primitives;

public class AuditableEntity : AggregateRoot
{
    public Guid CreatedUserId { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid? ModifiedUserId { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}