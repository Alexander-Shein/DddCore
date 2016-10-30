using System.Threading.Tasks;
using Contracts.Dal.DomainStack;
using Contracts.Domain.Entities;
using Contracts.Services.Application;
using Contracts.Services.Application.DomainStack;

namespace Services.Application.DomainStack
{
    public abstract class EntityServiceBase<T, TKey> : IEntityService<T, TKey> where T : class, IAggregateRootEntityBase<TKey>
    {
        #region Private Members

        readonly IRepository<T, TKey> repository;
        readonly IGuard guard;

        #endregion

        #region ctor

        protected EntityServiceBase(IRepository<T, TKey> repository, IGuard guard)
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

            repository.PersistEntityGraph(entity);
        }

        #endregion
    }
}
