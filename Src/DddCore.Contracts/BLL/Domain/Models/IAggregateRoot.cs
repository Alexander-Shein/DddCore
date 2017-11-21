using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.BLL.Domain.Models
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
        IMemento<TKey> GetMemento();
        Result RaiseEvents(IDomainEventDispatcher eventDispatcher);
    }
}