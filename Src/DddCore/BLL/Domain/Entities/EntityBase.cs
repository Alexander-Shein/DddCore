using System.Collections.Generic;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Domain.Events;

namespace DddCore.BLL.Domain.Entities
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        public CrudState CrudState { get; set; }
        public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();
    }
}