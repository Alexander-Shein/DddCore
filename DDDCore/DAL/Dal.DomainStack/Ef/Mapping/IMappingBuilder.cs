using System.Data.Entity.ModelConfiguration;
using Contracts.Domain.Entities;

namespace Dal.DomainStack.Ef.Mapping
{
    public interface IMappingBuilder
    {
        void Add<T, TKey>(AggregateRootEntityBaseMap<T, TKey> aggregateRootEntityBaseMap) where TKey : struct where T : class, IAggregateRootEntity<TKey>;
        void Add<T, TKey>(EntityBaseMap<T, TKey> entityBaseMap) where TKey : struct where T : class, IEntity<TKey>;
        void Add<T>(EntityTypeConfiguration<T> configuration) where T : class;
    }
}