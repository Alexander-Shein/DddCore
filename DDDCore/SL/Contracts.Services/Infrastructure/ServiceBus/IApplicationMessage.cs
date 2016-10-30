using Contracts.Domain.Events;

namespace Contracts.Services.Infrastructure.ServiceBus
{
    public interface IApplicationMessage : IDomainEvent
    {
        string EventType { get; }
    }
}