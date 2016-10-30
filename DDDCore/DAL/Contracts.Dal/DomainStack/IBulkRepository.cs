using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain.Entities;

namespace Contracts.Dal.DomainStack
{
    public interface IBulkRepository<in T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        Task BulkInsertAsync(IEnumerable<T> domains);
        Task BulkUpdateAsync(IEnumerable<T> domains);
        Task BulkDeleteAsync(IEnumerable<T> domains);
        Task BulkMergeAsync(IEnumerable<T> domains);
    }
}