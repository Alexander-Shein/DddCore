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
        public IDomainEventDispatcher DomainEventDispatcher { get; set; }
        public IBusinessRulesValidatorFactory BusinessRulesValidatorFactory { get; set; }

        public TKey Id { get; set; }
        public CrudState CrudState { get; set; }
        public ICollection<IDomainEvent> Events { get; } = new List<IDomainEvent>();

        public void RaiseEvents()
        {
            if (!Events.Any()) return;
            if (DomainEventDispatcher == null) throw new ArgumentNullException(nameof(DomainEventDispatcher));

            Events.Do(domainEvent => DomainEventDispatcher.Raise(domainEvent));
            Events.Clear();
        }

        public async Task<OperationResult> ValidateAsync()
        {
            var businessRulesValidator = BusinessRulesValidatorFactory.GetBusinessRulesValidator(this);
            var validationResult = await businessRulesValidator.ValidateAsync(this);

            return validationResult;
        }

        public OperationResult Validate()
        {
            var businessRulesValidator = BusinessRulesValidatorFactory.GetBusinessRulesValidator(this);
            var validationResult = businessRulesValidator.Validate(this);

            return validationResult;
        }
    }
}