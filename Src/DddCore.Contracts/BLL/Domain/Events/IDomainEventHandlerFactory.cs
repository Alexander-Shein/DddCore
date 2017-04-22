using System.Collections.Generic;

namespace DddCore.Contracts.BLL.Domain.Events
{
    public interface IDomainEventHandlerFactory
    {
        IEnumerable<IDomainEventHandler<T>> GetHandlers<T>() where T : IDomainEvent;
    }
}