using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Model;
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

        public void RaiseEvents(IDomainEventDispatcher domainEventDispatcher)
        {
            if (!Events.Any()) return;
            if (domainEventDispatcher == null) throw new ArgumentNullException(nameof(domainEventDispatcher));

            Events.Do(domainEventDispatcher.Raise);
            Events.Clear();
        }

        public async Task<OperationResult> ValidateAsync(IBusinessRulesValidatorFactory businessRulesValidatorFactory)
        {
            if (businessRulesValidatorFactory == null) throw new ArgumentNullException(nameof(businessRulesValidatorFactory));

            var businessRulesValidator = businessRulesValidatorFactory.GetBusinessRulesValidator(this);
            if (businessRulesValidator == null) return OperationResult.Succeed;

            var validationResult = await businessRulesValidator.ValidateAsync(this);

            return validationResult;
        }

        public OperationResult Validate(IBusinessRulesValidatorFactory businessRulesValidatorFactory)
        {
            if (businessRulesValidatorFactory == null) throw new ArgumentNullException(nameof(businessRulesValidatorFactory));

            var businessRulesValidator = businessRulesValidatorFactory.GetBusinessRulesValidator(this);
            if (businessRulesValidator == null) return OperationResult.Succeed;

            var validationResult = businessRulesValidator.Validate(this);

            return validationResult;
        }
    }
}