using System;
using System.Collections.Generic;
using DddCore.Contracts.Domain.Events;

namespace DddCore.Domain.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        #region Private Members

        readonly IDomainEventHandlerFactory handlerFactory;
        ICollection<Delegate> actions;

        #endregion

        public DomainEventDispatcher(IDomainEventHandlerFactory handlerFactory)
        {
            this.handlerFactory = handlerFactory;
        }

        #region Public Methods

        public void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }
            actions.Add(callback);
        }

        public void Raise<T>(T args) where T : IDomainEvent
        {
            foreach (var handler in handlerFactory.GetHandlers<T>())
            {
                handler.Handle(args);
            }

            if (actions == null) return;

            foreach (var action in actions)
            {
                var action1 = action as Action<T>;
                action1?.Invoke(args);
            }
        }

        #endregion
    }
}
