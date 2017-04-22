using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Errors;
using DddCore.Contracts.Services.Infrastructure;

namespace DddCore.Contracts.Services.Application.DomainStack
{
    public interface IGuard : IInfrastructureService
    {
        /// <summary>
        /// If null throws AgrumentNullException.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        void NotNull(object obj, string message = "");

        /// <summary>
        /// Retrives business rules validator and validates business rules for aggregateRoot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="aggregateRoot"></param>
        /// <returns>Business rules validation result</returns>
        OperationResult ValidateBusinessRules<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;

        /// <summary>
        /// Retrives business rules validator and validates business rules for aggregateRoot.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        Task<OperationResult> ValidateBusinessRulesAsync<T, TKey>(T aggregateRoot) where T : IAggregateRootEntity<TKey>;
    }
}
