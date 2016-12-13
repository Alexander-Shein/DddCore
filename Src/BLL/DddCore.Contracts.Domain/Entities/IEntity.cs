using DddCore.Contracts.Domain.Entities.Model;

namespace DddCore.Contracts.Domain.Entities
{
    public interface IEntity<TKey> : ICrudState, IKey<TKey>, IEvents
    {
    }
}