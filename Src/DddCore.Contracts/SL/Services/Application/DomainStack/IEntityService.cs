using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.SL.Services.Application.DomainStack
{
    public interface IEntityService<in T, in TKey> where T : class, IAggregateRootEntity<TKey>
    {
        /// <summary>
        /// Validate business rules, raise events and persist aggregate root graph.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns>if OperationResult.IsValid then aggregate root is persisted. If not BusinessRulesValidationResult.BrokenBusinessRules is populated.</returns>
        OperationResult ValidateAndPersist(T aggregateRoot);

        /// <summary>
        /// Async version of TryPersistAggregateRoot.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        Task<OperationResult> ValidateAndPersistAsync(T aggregateRoot);
    }
}
