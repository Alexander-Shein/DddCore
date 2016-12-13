using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities;

namespace DddCore.Contracts.Services.Application.DomainStack
{
    public interface IEntityService<in T, in TKey> where T : class, IAggregateRootEntity<TKey>
    {
        Task PersistAggregateRootAsync(T aggregateRoot);
    }
}
