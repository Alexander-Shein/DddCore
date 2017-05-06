using System.Linq;
using System.Reflection;
using DddCore.Contracts.BLL.Domain.Events;

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

        public void Raise<T>(T args) where T : IDomainEvent
        {
            var methodType =
                this
                    .GetType()
                    .GetMethod("SendMessage", BindingFlags.NonPublic | BindingFlags.Instance)
                    .MakeGenericMethod(args.GetType());

            methodType.Invoke(this, new object[] { args });
        }

        #endregion

        #region Private Methods

        private void SendMessage<T>(T args) where T : IDomainEvent
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
