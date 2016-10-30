using Contracts.Domain.Entities;
using Contracts.Domain.Entities.Model;

namespace Domain.Entities
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        public CrudState CrudState { get; set; }
    }
}
