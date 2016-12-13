using System.Threading.Tasks;
using DddCore.Contracts.Dal.DomainStack;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Events;
using DddCore.Contracts.Services.Application.DomainStack;

namespace DddCore.Services.Application.DomainStack
{
    public abstract class EntityServiceBase<T, TKey> : IEntityService<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        #region Private Members

        readonly IRepository<T, TKey> repository;
        readonly IGuard guard;
        readonly IDomainEventDispatcher domainEventDispatcher;

        #endregion

        #region ctor

        protected EntityServiceBase(IRepository<T, TKey> repository, IGuard guard, IDomainEventDispatcher domainEventDispatcher)
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
            await guard.AggregateRootIsValidAsync<T, TKey>(aggregateRoot);

            foreach (var domainEvent in aggregateRoot.Events)
            {
                domainEventDispatcher.Raise(domainEvent);
            }

            repository.PersistAggregateRoot(aggregateRoot);
        }

        #endregion
    }
}
