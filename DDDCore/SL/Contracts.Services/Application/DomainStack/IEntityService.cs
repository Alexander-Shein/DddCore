using System.Threading.Tasks;
using Contracts.Domain.Entities;

namespace Contracts.Services.Application.DomainStack
{
    public interface IEntityService<in T, in TKey> where T : class, IAggregateRootEntity<TKey>
    {
        Task PersistEntityGraphAsync(T entity);
    }
}
