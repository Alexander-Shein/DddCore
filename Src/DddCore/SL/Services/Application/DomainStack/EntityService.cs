using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Errors;
using DddCore.Contracts.DAL.DomainStack;
using DddCore.Contracts.SL.Services.Application.DomainStack;

namespace DddCore.SL.Services.Application.DomainStack
{
    public class EntityService<T, TKey> : IEntityService<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        #region Private Members

        readonly IRepository<T, TKey> repository;
        readonly IGuard guard;

        #endregion

        #region ctor

        public EntityService(IRepository<T, TKey> repository, IGuard guard)
        {
            this.repository = repository;
            this.guard = guard;
        }

        #endregion

        #region Public Methods

        public virtual OperationResult ValidateAndPersist(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);

            var validationResult = aggregateRoot.Validate();

            if (validationResult.IsNotSucceed) return validationResult;

            aggregateRoot.RaiseEvents();
            repository.Persist(aggregateRoot);
            return validationResult;
        }

        public virtual async Task<OperationResult> ValidateAndPersistAsync(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);

            var validationResult = await aggregateRoot.ValidateAsync();

            if (validationResult.IsNotSucceed) return validationResult;

            aggregateRoot.RaiseEvents();
            repository.Persist(aggregateRoot);
            return validationResult;
        }

        #endregion
    }
}