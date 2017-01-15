using System;
using System.Collections.Generic;
using DddCore.Contracts.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Domain.Events
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        #region Private Members

        readonly IServiceProvider serviceProvider;

        #endregion

        public DomainEventHandlerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        #region Public Methods

        public IEnumerable<IDomainEventHandler<T>> GetHandlers<T>() where T : IDomainEvent
        {
            return serviceProvider.GetService<IEnumerable<IDomainEventHandler<T>>>();
        }

        #endregion
    }
}