using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities;

namespace DddCore.Contracts.Services.Application.DomainStack
{
    public interface IEntityService<in T, in TKey> where T : class, IAggregateRootEntity<TKey>
    {
        /// <summary>
        /// Validate, raise events and persist aggregate root graph
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        Task PersistAggregateRootAsync(T aggregateRoot);

        /// <summary>
        /// Validate, raise events and persist aggregate root graph
        /// </summary>
        /// <param name="aggregateRoot"></param>
        /// <returns></returns>
        void PersistAggregateRoot(T aggregateRoot);
    }
}
