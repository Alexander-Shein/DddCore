using System.Reflection;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;

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
            var methodType =
                this
                    .GetType()
                    .GetMethod("SendMessage", BindingFlags.NonPublic | BindingFlags.Instance)
                    .MakeGenericMethod(args.GetType());

            OperationResult result = (OperationResult) methodType.Invoke(this, new object[] { args });
            return result;
        }

        #endregion

        #region Private Methods

        private OperationResult SendMessage<T>(T args) where T : IDomainEvent
        {
            var handlers = domainEventHandlerFactory.GetHandlers<T>();

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
