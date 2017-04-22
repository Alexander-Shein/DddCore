using System.Collections.Generic;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Contracts.Domain.Events;

namespace DddCore.Domain.Entities
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        public CrudState CrudState { get; set; }
        public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();
    }
}