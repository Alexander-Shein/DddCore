using DddCore.Contracts.Domain.Events;

namespace DddCore.Contracts.Services.Infrastructure.ServiceBus
{
    public interface IApplicationMessage : IDomainEvent
    {
        string EventType { get; }
    }
}