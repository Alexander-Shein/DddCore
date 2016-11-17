using Contracts.Domain.Entities.BusinessRules;
using Contracts.Domain.Entities.Model;

namespace Contracts.Domain.Entities
{
    public interface IAggregateRootEntity<TKey> :
        IEntity<TKey>,
        IEntityGraph,
        IVersion,
        IValidatable,
        IEvents,
        IPublicKey
    {
    }
}