using DddCore.Contracts.BLL.Domain.Events;
using System.Linq;
using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.BLL.Domain.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        #region Private Members

        private readonly IDomainEventHandlerFactory _domainEventHandlerFactory;

        #endregion

        public DomainEventDispatcher(IDomainEventHandlerFactory domainEventHandlerFactory)
        {
            _domainEventHandlerFactory = domainEventHandlerFactory;
        }

        #region Public Methods

        public Result Raise<T>(T args) where T : IDomainEvent
        {
            var handlers = _domainEventHandlerFactory.GetHandlers<T>();
            if (!handlers.Any()) return Result.Success;

            foreach (var handler in handlers)
            {
                var result = handler.Handle(args);
                if (result.IsFailure) return result;
            }

            return Result.Success;
        }

        #endregion
    }
}
