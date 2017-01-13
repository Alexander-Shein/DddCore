using System.Threading.Tasks;
using DddCore.Contracts.Dal.DomainStack;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Domain.Events;
using DddCore.Contracts.Services.Application.DomainStack;
using DddCore.Crosscutting;
using System.Linq;

namespace DddCore.Services.Application.DomainStack
{
    public class EntityService<T, TKey> : IEntityService<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        #region Private Members

        readonly IRepository<T, TKey> repository;
        readonly IGuard guard;
        readonly IDomainEventDispatcher domainEventDispatcher;

        #endregion

        #region ctor

        public EntityService(IRepository<T, TKey> repository, IGuard guard, IDomainEventDispatcher domainEventDispatcher)
        {
            this.repository = repository;
            this.guard = guard;
            this.domainEventDispatcher = domainEventDispatcher;
        }

        #endregion

        #region Public Methods

        public virtual void PersistAggregateRoot(T aggregateRoot)
        {
            var validationResult = TryPersistAggregateRoot(aggregateRoot);

            if (validationResult.IsNotValid)
            {
                throw validationResult.BrokenBusinessRules.First();
            }
        }

        public virtual async Task PersistAggregateRootAsync(T aggregateRoot)
        {
            var validationResult = await TryPersistAggregateRootAsync(aggregateRoot);

            if (validationResult.IsNotValid)
            {
                throw validationResult.BrokenBusinessRules.First();
            }
        }

        public virtual BusinessRulesValidationResult TryPersistAggregateRoot(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);

            var validationResult = guard.ValidateBusinessRules<T, TKey>(aggregateRoot);

            if (validationResult.IsValid)
            {
                RaiseEvents(aggregateRoot);
                repository.PersistAggregateRoot(aggregateRoot);
            }

            return validationResult;
        }

        public virtual async Task<BusinessRulesValidationResult> TryPersistAggregateRootAsync(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);

            var validationResult = await guard.ValidateBusinessRulesAsync<T, TKey>(aggregateRoot);

            if (validationResult.IsValid)
            {
                RaiseEvents(aggregateRoot);
                repository.PersistAggregateRoot(aggregateRoot);
            }

            return validationResult;
        }

        #endregion

        #region Private Methods

        void RaiseEvents(T aggregateRoot)
        {
            aggregateRoot.WalkAggregateRootGraph(entity =>
            {
                entity.Events.Do(domainEvent => domainEventDispatcher.Raise(domainEvent));
                entity.Events.Clear();
            });
        }

        #endregion
    }
}