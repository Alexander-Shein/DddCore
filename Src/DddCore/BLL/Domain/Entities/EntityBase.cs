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
            Guard.ThrowIfNull(factory, nameof(factory));

            dynamic obj = this;
            var validator = factory.GetBusinessRulesValidator(obj);

            if (validator == null) return OperationResult.Succeed;

            var result = await validator.ValidateAsync(obj);
            return result;
        }

        public virtual OperationResult Validate(IBusinessRulesValidatorFactory factory)
        {
            Guard.ThrowIfNull(factory, nameof(factory));

            dynamic obj = this;
            var validator = factory.GetBusinessRulesValidator(obj);

            if (validator == null) return OperationResult.Succeed;

            var result = validator.Validate(obj);
            return result;
        }
    }
}