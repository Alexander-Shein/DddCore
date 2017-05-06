using System.Collections.Generic;
using DddCore.Contracts.BLL.Domain.Events;

namespace DddCore.Contracts.BLL.Domain.Entities.Model
{
    public interface IDomainEvents
    {
        ICollection<IDomainEvent> Events { get; }
        void RaiseEvents(GraphDepth graphDepth = GraphDepth.AggregateRoot);
    }
}