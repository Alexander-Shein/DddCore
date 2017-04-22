using System.Linq;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.DAL.DomainStack;
using DddCore.Contracts.SL.Services.Application.DomainStack;
using DddCore.Crosscutting;

namespace DddCore.SL.Services.Application.DomainStack
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

        //public virtual void PersistAggregateRoot(T aggregateRoot)
        //{
        //    var validationResult = TryPersistAggregateRoot(aggregateRoot);
        //
        //    if (validationResult.IsNotValid)
        //    {
        //        throw validationResult.Errors.First();
        //    }
        //}

        //public virtual async Task PersistAggregateRootAsync(T aggregateRoot)
        //{
        //    var validationResult = await TryPersistAggregateRootAsync(aggregateRoot);
        //
        //    if (validationResult.IsNotValid)
        //    {
        //        throw validationResult.BrokenBusinessRules.First();
        //    }
        //}

        public virtual OperationResult ValidateAndPersist(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);

            var validationResult = guard.ValidateBusinessRules<T, TKey>(aggregateRoot);

            if (validationResult.IsSucceed)
            {
                RaiseEvents(aggregateRoot);
                repository.Persist(aggregateRoot);
            }

            return validationResult;
        }

        public virtual async Task<OperationResult> ValidateAndPersistAsync(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);

            var validationResult = await guard.ValidateBusinessRulesAsync<T, TKey>(aggregateRoot);

            if (validationResult.IsSucceed)
            {
                RaiseEvents(aggregateRoot);
                repository.Persist(aggregateRoot);
            }

            return validationResult;
        }

        #endregion

        #region Private Methods

        void RaiseEvents(T aggregateRoot)
        {
            aggregateRoot.WalkAggregateRootGraph(entity =>
            {
                if (entity.Events.Any())
                {
                    entity.Events.Do(domainEvent => domainEventDispatcher.Raise(domainEvent));
                    entity.Events.Clear();
                }
            });
        }

        #endregion
    }
}