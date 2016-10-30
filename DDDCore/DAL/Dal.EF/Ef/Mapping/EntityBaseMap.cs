using System.Data.Entity.ModelConfiguration;
using Contracts.Domain.Entities;

namespace Dal.DomainStack.Ef.Mapping
{
    public abstract class EntityBaseMap<T, TKey> : EntityTypeConfiguration<T> where T : class, IEntity<TKey> where TKey : struct
    {
        protected EntityBaseMap()
        {
            HasKey(x => x.Id);

            Ignore(x => x.CrudState);
        }
    }
}
