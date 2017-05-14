using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IAggregateRootEntity<TKey> : IEntity<TKey>, IEntityGraph<TKey>
    {
        void RaiseEvents(IDomainEventDispatcher domainEventDispatcher, GraphDepth graphDepth = GraphDepth.AggregateRoot);
        Task<OperationResult> ValidateAsync(IBusinessRulesValidatorFactory businessRulesValidatorFactory, GraphDepth graphDepth = GraphDepth.AggregateRoot);
        OperationResult Validate(IBusinessRulesValidatorFactory businessRulesValidatorFactory, GraphDepth graphDepth = GraphDepth.AggregateRoot);
    }
}