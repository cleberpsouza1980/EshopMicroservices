using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvents : INotification
{
    Guid EventId => Guid.NewGuid();

    public DateTime OccurredOn => DateTime.Now;

    public string EventyType => GetType().AssemblyQualifiedName ?? string.Empty;
}
