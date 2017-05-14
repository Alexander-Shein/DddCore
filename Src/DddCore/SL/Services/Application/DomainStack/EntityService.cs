using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Events;
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
        readonly IBusinessRulesValidatorFactory businessRulesValidatorFactory;
        readonly IDomainEventDispatcher domainEventDispatcher;

        #endregion

        #region ctor

        public EntityService(
            IRepository<T, TKey> repository,
            IGuard guard,
            IBusinessRulesValidatorFactory businessRulesValidatorFactory,
            IDomainEventDispatcher domainEventDispatcher)
        {
            this.repository = repository;
            this.guard = guard;
            this.businessRulesValidatorFactory = businessRulesValidatorFactory;
            this.domainEventDispatcher = domainEventDispatcher;
        }

        #endregion

        #region Public Methods

        public virtual OperationResult ValidateAndPersist(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);

            var validationResult = aggregateRoot.Validate(businessRulesValidatorFactory);

            if (validationResult.IsNotSucceed) return validationResult;

            aggregateRoot.RaiseEvents(domainEventDispatcher);
            repository.Persist(aggregateRoot);
            return validationResult;
        }

        public virtual async Task<OperationResult> ValidateAndPersistAsync(T aggregateRoot)
        {
            guard.NotNull(aggregateRoot);

            var validationResult = await aggregateRoot.ValidateAsync(businessRulesValidatorFactory);

            if (validationResult.IsNotSucceed) return validationResult;

            aggregateRoot.RaiseEvents(domainEventDispatcher);
            repository.Persist(aggregateRoot);
            return validationResult;
        }

        #endregion
    }
}