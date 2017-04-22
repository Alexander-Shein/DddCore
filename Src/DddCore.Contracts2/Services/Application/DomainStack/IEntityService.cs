using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Entities.BusinessRules;

namespace DddCore.Contracts.Services.Application.DomainStack
{
    public interface IEntityService<in T, in TKey> where T : class, IAggregateRootEntity<TKey>
    {
        /// <summary>
        /// Validate business rules, raise events and persist aggregate root graph.
        /// Throws first broken business rule if any.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        void PersistAggregateRoot(T aggregateRoot);

        /// <summary>
        /// Async version of PersistAggregateRoot.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        Task PersistAggregateRootAsync(T aggregateRoot);

        /// <summary>
        /// Validate business rules, raise events and persist aggregate root graph.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns>if BusinessRulesValidationResult.IsValid then aggregate root is persisted. If not BusinessRulesValidationResult.BrokenBusinessRules is populated.</returns>
        BusinessRulesValidationResult TryPersistAggregateRoot(T aggregateRoot);

        /// <summary>
        /// Async version of TryPersistAggregateRoot.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        Task<BusinessRulesValidationResult> TryPersistAggregateRootAsync(T aggregateRoot);
    }
}
