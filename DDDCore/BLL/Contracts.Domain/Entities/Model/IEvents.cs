using System.Collections.Generic;
using Contracts.Domain.Events;

namespace Contracts.Domain.Entities.Model
{
    public interface IEvents
    {
        ICollection<IDomainEvent> Events { get; }
    }
}