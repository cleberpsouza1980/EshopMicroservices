namespace Ordering.Domain.Abstractions
{

    public interface IAggregate<T>:IAggregate,IEntity<T>
    {
        
    }
    public interface IAggregate : IEntity
    {
        public IReadOnlyList<IDomainEvents> DomainEvents { get; }

        IDomainEvents[] ClearDomainEvents();
    }
}
