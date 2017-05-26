using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;
using DddCore.Crosscutting;

namespace DddCore.BLL.Domain.Entities
{
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        public CrudState CrudState { get; set; }
        public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();

        public virtual OperationResult RaiseEvents(IDomainEventDispatcher eventDispatcher)
        {
            Guard.ThrowIfNull(eventDispatcher, nameof(eventDispatcher));

            if (!Events.Any()) return OperationResult.Succeed;

            foreach (dynamic domainEvent in Events)
            {
                var result = eventDispatcher.Raise(domainEvent);
                if (result.IsNotSucceed) return result;
            }

            Events.Clear();
            return OperationResult.Succeed;
        }

        public virtual async Task<OperationResult> ValidateAsync(IBusinessRulesValidatorFactory factory)
        {
            dynamic obj = this;
            var validator = GetValidator(factory, obj);

            var result = await validator.ValidateAsync(this);
            return result;
        }

        public virtual OperationResult Validate(IBusinessRulesValidatorFactory factory)
        {
            dynamic obj = this;
            var validator = GetValidator(factory, obj);

            var result = validator.Validate(this);
            return result;
        }

        #region Private Methods

        private IBusinessRulesValidator<T> GetValidator<T>(IBusinessRulesValidatorFactory factory, T obj) where T : ICrudState
        {
            Guard.ThrowIfNull(factory, nameof(factory));
            var validator = factory.GetBusinessRulesValidator(obj);

            Guard.ThrowIfNull(validator, $"Cannot find business rules validator for '{GetType().Name}'.");

            return validator;
        }

        #endregion
    }
}