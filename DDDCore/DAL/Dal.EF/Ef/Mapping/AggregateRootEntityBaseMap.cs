using Contracts.Domain.Entities;

namespace Dal.DomainStack.Ef.Mapping
{
    public abstract class AggregateRootEntityBaseMap<T, TKey> : EntityBaseMap<T, TKey> where T : class, IAggregateRootEntity<TKey> where TKey : struct
    {
        protected AggregateRootEntityBaseMap()
        {
            Property(x => x.CreatedAt).IsRequired();
            Property(x => x.ModifiedAt).IsRequired();
            Property(x => x.Ts).IsRowVersion().IsRequired();
            Property(x => x.IsDeleted).IsRequired();

            Ignore(x => x.ValidationErrors);
        }
    }
}