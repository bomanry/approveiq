using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
