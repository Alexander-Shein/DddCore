using System.Collections.Generic;
using System.Linq;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Domain.Models;
using DddCore.Contracts.BLL.Domain.Services;
using DddCore.Crosscutting;

namespace DddCore.BLL.Domain.Models
{
    public abstract class AggregateRootBase<TKey, TState> : IAggregateRoot<TKey, TState> where TState : IAggregateRootState<TKey>, new()
    {
        protected AggregateRootBase()
        {
            State = new TState();
        }

        protected AggregateRootBase(TState state)
        {
            Guard.NotNull(state, nameof(state));
            State = state;
        }

        public TKey Id => State.Id;

        #region Public Methods

        public Result RaiseEvents(IDomainEventDispatcher eventDispatcher)
        {
            Guard.NotNull(eventDispatcher, nameof(eventDispatcher));

            if (Events.Any())
            {
                foreach (dynamic domainEvent in Events)
                {
                    //var result = eventDispatcher.Raise(domainEvent);
                    //if (result.IsNotSucceed) return result;
                }

                Events.Clear();
            }

            return Result.Success;
        }

        public virtual TState GetState()
        {
            return State;
        }

        #endregion

        protected ICollection<IDomainEvent> Events { get; set; } = new List<IDomainEvent>();
        protected TState State { get; set; }
        //protected abstract IBusinessRulesValidator<T> Validator { get; } where T: AggregateRootBase<TKey, TMemento>

        //private IDictionary<>
    }
}
