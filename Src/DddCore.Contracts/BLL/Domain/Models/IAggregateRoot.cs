using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.BLL.Domain.Models
{
    public interface IAggregateRoot<out TKey, out TState> : IEntity<TKey> where TState : IAggregateRootState<TKey>
    {
        TState GetState();

        Result RaiseEvents(IDomainEventDispatcher eventDispatcher);
    }
}