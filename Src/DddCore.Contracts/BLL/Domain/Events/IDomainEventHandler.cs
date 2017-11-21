using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.BLL.Domain.Events
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        Result Handle(T domainEvent);
    }
}