using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Graph;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>, IEntityGraph<TKey>
    {
        OperationResult RaiseGraphEvents(IDomainEventDispatcher domainEventDispatcher, GraphDepth graphDepth = GraphDepth.AggregateRoot);

        Task<OperationResult> ValidateGraphAsync(IBusinessRulesValidatorFactory factory, GraphDepth graphDepth = GraphDepth.AggregateRoot);
        OperationResult ValidateGraph(IBusinessRulesValidatorFactory factory, GraphDepth graphDepth = GraphDepth.AggregateRoot);
    }
}