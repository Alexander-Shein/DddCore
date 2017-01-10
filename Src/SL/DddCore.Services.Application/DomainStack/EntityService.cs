using System.Threading.Tasks;
using DddCore.Contracts.Dal.DomainStack;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Events;
using DddCore.Contracts.Services.Application.DomainStack;
using DddCore.Crosscutting;

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

        public async Task PersistAggregateRootAsync(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);
            await guard.ValidateAggregateRootAndThrowAsync<T, TKey>(aggregateRoot);

            RaiseEvents(aggregateRoot);
            repository.PersistAggregateRoot(aggregateRoot);
        }

        public void PersistAggregateRoot(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);
            guard.ValidateAggregateRootAndThrow<T, TKey>(aggregateRoot);

            RaiseEvents(aggregateRoot);
            repository.PersistAggregateRoot(aggregateRoot);
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