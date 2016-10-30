using Contracts.Domain.Entities.BusinessRules;
using Contracts.Domain.Entities.Model;

namespace Contracts.Domain.Entities
{
    public interface IAggregateRootEntity<TKey> :
        IEntity<TKey>,
        IVersion,
        IValidatable,
        IEvents,
        IPublicKey
    {
    }
}