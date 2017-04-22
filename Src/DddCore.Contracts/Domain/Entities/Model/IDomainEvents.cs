using System.Collections.Generic;
using DddCore.Contracts.Domain.Events;

namespace DddCore.Contracts.Domain.Entities.Model
{
    public interface IDomainEvents
    {
        ICollection<IDomainEvent> Events { get; }
    }
}