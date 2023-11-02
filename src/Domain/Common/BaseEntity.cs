using System.ComponentModel.DataAnnotations.Schema;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Common;

public abstract class BaseEntity : IEntityDomainEvent
{
    private readonly List<BaseEvent> _domainEvents = new();

    public string Id { get; private set; } = Guid.NewGuid().ToString();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

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
}
