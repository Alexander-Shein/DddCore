using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Contracts.Domain.Entities;

namespace Dal.DomainStack.Ef.Mapping
{
    public class MappingBuilder : IMappingBuilder
    {
        #region Private Members

        readonly DbModelBuilder dbModelBuilder;

        #endregion

        public MappingBuilder(DbModelBuilder dbModelBuilder)
        {
            this.dbModelBuilder = dbModelBuilder;
        }

        #region Public Methods

        public void Add<T, TKey>(AggregateRootEntityBaseMap<T, TKey> aggregateRootEntityBaseMap) where T : class, IAggregateRootEntity<TKey> where TKey : struct
        {
            dbModelBuilder.Configurations.Add(aggregateRootEntityBaseMap);
        }

        public void Add<T, TKey>(EntityBaseMap<T, TKey> entityBaseMap) where T : class, IEntity<TKey> where TKey : struct
        {
            dbModelBuilder.Configurations.Add(entityBaseMap);
        }

        public void Add<T>(EntityTypeConfiguration<T> configuration) where T : class
        {
            dbModelBuilder.Configurations.Add(configuration);
        }

        #endregion
    }
}