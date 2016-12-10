using System;

namespace DddCore.Contracts.Domain.Events
{
    public interface IDomainEventDispatcher
    {
        void Register<T>(Action<T> callback) where T : IDomainEvent;
        void Raise<T>(T args) where T : IDomainEvent;
    }
}