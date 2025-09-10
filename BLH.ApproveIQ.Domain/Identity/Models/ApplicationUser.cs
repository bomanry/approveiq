using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using BLH.ApproveIQ.Domain.Primitives;

namespace BLH.ApproveIQ.Domain.Identity.Models;

public class ApplicationUser : IdentityUser, IEventable
{
    [IgnoreDataMember]
    private readonly List<IDomainEvent> _domainEvents = new();

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTimeOffset? LastLoginDate { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; }

    [IgnoreDataMember]
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}
