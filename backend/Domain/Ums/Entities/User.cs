using System.ComponentModel.DataAnnotations.Schema;
using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Domain.Ums.Entities;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    [EnableSearch]
    public override required string UserName { get; set; }
    
    [EnableSearch]
    public override required string Email { get; set; }
    
    [EnableSearch]
    public override string? PhoneNumber { get; set; }

    [EnableSearch]
    public string? FullName { get; set; }
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool Status { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public List<UserRole> UserRoles { get; set; }

    #region Domain Event

    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped] public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    #endregion
}