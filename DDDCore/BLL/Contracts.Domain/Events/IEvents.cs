using System.Collections.Generic;

namespace Contracts.Domain.Events
{
    public interface IEvents
    {
        ICollection<IDomainEvent> Events { get; }
    }
}