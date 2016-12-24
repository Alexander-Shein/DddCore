using System.Threading.Tasks;
using DddCore.Contracts.Dal.DomainStack;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Events;
using DddCore.Contracts.Services.Application.DomainStack;

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
            await guard.AggregateRootIsValidAsync<T, TKey>(aggregateRoot);

            aggregateRoot.WalkAggregateRootGraph(entity =>
            {
                foreach (var domainEvent in entity.Events)
                {
                    domainEventDispatcher.Raise(domainEvent);
                }
            });

            repository.PersistAggregateRoot(aggregateRoot);
        }

        #endregion
    }
}
