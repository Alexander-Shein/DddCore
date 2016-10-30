using Contracts.Domain.Entities.Model;
using Contracts.Domain.Entities.Validation;
using Contracts.Domain.Events;

namespace Contracts.Domain.Entities
{
    public interface IAggregateRootEntity<TKey> :
        IEntity<TKey>,
        IVersion,
        IValidation,
        IEvents,
        IPublicKey
    {
    }
}