using System.Threading.Tasks;
using Contracts.Crosscutting.Ioc;
using Contracts.Dal.DomainStack;
using Contracts.Domain.Entities;
using Contracts.Services.Application.DomainStack;
using Domain.Events;

namespace Services.Application.DomainStack
{
    public abstract class EntityServiceBase<T, TKey> : IEntityService<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        #region Private Members

        readonly IRepository<T, TKey> repository;
        readonly IGuard guard;

        #endregion

        #region ctor

        protected EntityServiceBase(IRepository<T, TKey> repository, IGuard guard, IContainer container)
        {
            this.repository = repository;
            this.guard = guard;
        }

        #endregion

        #region Public Methods

        public async Task PersistEntityGraphAsync(T entity)
        {
            guard.NotNull(entity);
            await guard.DomainIsValidAsync(entity);

            foreach (var domainEvent in entity.Events)
            {
                DomainEvents.Raise(domainEvent);
            }

            repository.PersistEntityGraph(entity);
        }

        #endregion
    }
}
