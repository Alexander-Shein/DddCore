using DddCore.Contracts.BLL.Domain.Events;
using System;

namespace DddCore.BLL.Domain.Events
{
    public class DomainEventBase : IDomainEvent
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
