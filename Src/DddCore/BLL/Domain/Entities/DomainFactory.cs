using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Events;

namespace DddCore.BLL.Domain.Entities
{
    public class DomainFactory : IDomainFactory
    {
        readonly IDomainEventDispatcher domainEventDispatcher;
        readonly IBusinessRulesValidatorFactory businessRulesValidatorFactory;

        public DomainFactory(IDomainEventDispatcher domainEventDispatcher, IBusinessRulesValidatorFactory businessRulesValidatorFactory)
        {
            this.domainEventDispatcher = domainEventDispatcher;
            this.businessRulesValidatorFactory = businessRulesValidatorFactory;
        }

        public T Create<T, TKey>() where T : IEntity<TKey>, new()
        {
            var domain = new T();
            var entityBase = domain as EntityBase<TKey>;

            entityBase.DomainEventDispatcher = domainEventDispatcher;
            entityBase.BusinessRulesValidatorFactory = businessRulesValidatorFactory;

            return domain;
        }
    }
}