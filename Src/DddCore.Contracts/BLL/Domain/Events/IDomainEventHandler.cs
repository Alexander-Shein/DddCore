using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Events
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        OperationResult Handle(T args);
    }
}