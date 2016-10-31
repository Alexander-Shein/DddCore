using System.Threading.Tasks;
using Contracts.Crosscutting.IoC;
using Contracts.Dal.DomainStack;
using Contracts.Domain.Entities;
using Contracts.Domain.Events;
using Contracts.Services.Application;
using Contracts.Services.Application.DomainStack;

namespace Services.Application.DomainStack
{
    public abstract class EntityServiceBase<T, TKey> : IEntityService<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        #region Private Members

        readonly IRepository<T, TKey> repository;
        readonly IGuard guard;
        readonly IContainer container;

        #endregion

        #region ctor

        protected EntityServiceBase(IRepository<T, TKey> repository, IGuard guard, IContainer container)
        {
            this.repository = repository;
            this.guard = guard;
            this.container = container;
        }

        #endregion

        #region Public Methods

        public async Task PersistEntityGraphAsync(T entity)
        {
            guard.NotNull(entity);
            await guard.DomainIsValidAsync(entity);

            foreach (var domainEvent in entity.Events)
            {
                //var handlers = container.ResolveAll<IHandle<domainEvent>>();
                //
                //for
            }

            repository.PersistEntityGraph(entity);
        }

        #endregion
    }
}
