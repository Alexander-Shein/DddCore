using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;

namespace DddCore.Contracts.DAL.DomainStack
{
    public interface IRepository<T, in TKey> where T : class, IAggregateRoot<TKey>
    {
        /// <summary>
        /// Save all entities in aggregate root to DataBase. Sync CrudState in all entities from the aggregate root. If graph contains links to other aggregate roots they will be skipped.
        /// </summary>
        /// <param name="aggregateRoot"></param>
        void Persist(T aggregateRoot);

        /// <summary>
        /// Read aggregate root with all related entities but w/o other aggregate roots.
        /// </summary>
        /// <param name="aggregateRootKey"></param>
        /// <returns>Aggregate root graph</returns>
        Task<T> GetByIdAsync(TKey aggregateRootKey);

        /// <summary>
        /// Read aggregate root with all related entities but w/o other aggregate roots.
        /// </summary>
        /// <param name="aggregateRootKey"></param>
        /// <returns>Aggregate root graph</returns>
        T GetById(TKey aggregateRootKey);
    }
}