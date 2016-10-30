using Contracts.Domain.Entities;

namespace Dal.DomainStack.Ef.Mapping
{
    public abstract class AggregateRootEntityBaseMap<T, TKey> : EntityBaseMap<T, TKey> where T : class, IAggregateRootEntity<TKey> where TKey : struct
    {
        protected AggregateRootEntityBaseMap()
        {
            Property(x => x.Ts).IsRowVersion().IsRequired();
        }
    }
}