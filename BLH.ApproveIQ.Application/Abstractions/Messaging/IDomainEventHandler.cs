using MediatR;
using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
