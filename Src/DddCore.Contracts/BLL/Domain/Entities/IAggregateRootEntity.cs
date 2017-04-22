using DddCore.Contracts.BLL.Domain.Entities.Model;

namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IAggregateRootEntity<TKey> : IEntity<TKey>, IEntityGraph<TKey>
    {
    }
}