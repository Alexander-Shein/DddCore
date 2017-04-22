using DddCore.Contracts.BLL.Domain.Events;

namespace DddCore.Contracts.SL.Services.Infrastructure.ServiceBus
{
    public interface IApplicationMessage : IDomainEvent
    {
        string EventType { get; }
    }
}