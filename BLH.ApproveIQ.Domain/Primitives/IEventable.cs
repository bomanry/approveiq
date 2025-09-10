namespace BLH.ApproveIQ.Domain.Primitives;

public interface IEventable
{
    IReadOnlyCollection<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
    void RaiseDomainEvent(IDomainEvent domainEvent);
}
