using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain.Entities;

namespace Contracts.Dal.DomainStack
{
    public interface IBulkRepository<in T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        Task BulkPersistEntityGraphAsync(IEnumerable<T> domains);
    }
}