using DddCore.Contracts.Domain.Events;

namespace DddCore.Domain.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        #region Private Members

        readonly IDomainEventHandlerFactory domainEventHandlerFactory;

        #endregion

        public DomainEventDispatcher(IDomainEventHandlerFactory domainEventHandlerFactory)
        {
            this.domainEventHandlerFactory = domainEventHandlerFactory;
        }

        #region Public Methods

        public void Raise<T>(T args) where T : IDomainEvent
        {
            var handlers = domainEventHandlerFactory.GetHandlers<T>();

            foreach (var handler in handlers)
            {
                handler.Handle(args);
            }
        }

        #endregion
    }
}
