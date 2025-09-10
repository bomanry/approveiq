using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.DomainEvents;

public sealed record AssignedTutorEvent(Guid Id, Guid SessionId, Guid TutorId) : IDomainEvent;