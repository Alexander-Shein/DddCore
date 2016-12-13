using System.Collections.Generic;
using DddCore.Contracts.Crosscutting.Ioc;
using DddCore.Contracts.Domain.Events;

namespace DddCore.Domain.Events
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        #region Private Members

        readonly IContainer container;

        #endregion

        public DomainEventHandlerFactory(IContainer container)
        {
            this.container = container;
        }

        #region Public Methods

        public IEnumerable<IDomainEventHandler<T>> GetHandlers<T>() where T : IDomainEvent
        {
            return container.ResolveAll<IDomainEventHandler<T>>();
        }

        #endregion
    }
}