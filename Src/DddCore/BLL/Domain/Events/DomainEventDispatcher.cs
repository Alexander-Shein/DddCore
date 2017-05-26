using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;
using System.Linq;

namespace DddCore.BLL.Domain.Events
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

        public OperationResult Raise<T>(T args) where T : IDomainEvent
        {
            var handlers = domainEventHandlerFactory.GetHandlers<T>();
            if (!handlers.Any()) return OperationResult.Succeed;

            foreach (var handler in handlers)
            {
                var result = handler.Handle(args);
                if (result.IsNotSucceed) return result;
            }

            return OperationResult.Succeed;
        }

        #endregion
    }
}
