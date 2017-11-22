using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.BLL.Domain.Models
{
    public interface IAggregateRoot<out TKey, out TMemento> : IEntity<TKey> where TMemento: IMemento<TKey>
    {
        TMemento GetMemento();
        Result RaiseEvents(IDomainEventDispatcher eventDispatcher);
    }
}