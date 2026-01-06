
namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{

    private readonly List<IDomainEvents> _domainEvents = new();
    public IReadOnlyList<IDomainEvents> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvents domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public IDomainEvents[] ClearDomainEvents()
    {
        IDomainEvents[] events = _domainEvents.ToArray();

        _domainEvents.Clear();

        return events;
    }
}
