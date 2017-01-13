using DddCore.Contracts.Domain.Entities.Model;

namespace DddCore.Contracts.Domain.Entities
{
    public interface IAggregateRootEntity<TKey> : IEntity<TKey>, IEntityGraph<TKey>
    {
    }
}