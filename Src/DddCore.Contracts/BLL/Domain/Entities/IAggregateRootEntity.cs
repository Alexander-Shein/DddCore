using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.BLL.Domain.Entities
{
    public interface IAggregateRootEntity<TKey> : IEntity<TKey>, IEntityGraph<TKey>
    {
        void RaiseEvents(GraphDepth graphDepth = GraphDepth.AggregateRoot);
        Task<OperationResult> ValidateAsync(GraphDepth graphDepth = GraphDepth.AggregateRoot);
        OperationResult Validate(GraphDepth graphDepth = GraphDepth.AggregateRoot);
    }
}