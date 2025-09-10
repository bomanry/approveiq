using MediatR;

namespace BLH.ApproveIQ.Domain.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
