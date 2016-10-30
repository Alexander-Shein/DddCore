using Contracts.Domain.Entities.Model;

namespace Contracts.Domain.Entities
{
    public interface IEntity<TKey> : ICrudState, IId<TKey>
    {
    }
}