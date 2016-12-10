using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities;

namespace DddCore.Contracts.Dal.DomainStack
{
    public interface IRepository<T, in TKey> where T : class, IAggregateRootEntity<TKey>
    {
        void PersistAggregateRoot(T entity);
        Task<T> ReadAggregateRootAsync(TKey key);
    }
}
