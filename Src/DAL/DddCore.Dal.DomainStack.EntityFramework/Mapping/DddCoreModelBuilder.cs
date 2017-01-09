using System.Reflection;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Crosscutting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddCore.Dal.DomainStack.EntityFramework.Mapping
{
    public class DddCoreModelBuilder : IModelBuilder
    {
        #region Private Members

        readonly ModelBuilder modelBuilder;

        #endregion

        public DddCoreModelBuilder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        #region Public Methods

        public EntityTypeBuilder<TEntity> Entity<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);

            var entityTypeBuilder = modelBuilder.Entity<TEntity>();

            if (type.IsAssignableFromGenericType(typeof(IKey<>)))
            {
                entityTypeBuilder.HasKey("Id");
            }

            if (type.IsAssignableFrom(typeof(IVersion)))
            {
                entityTypeBuilder.Property("Ts").IsRowVersion();
            }

            if (type.IsAssignableFrom(typeof(ICrudState)))
            {
                entityTypeBuilder.Ignore("CrudState");
            }

            return entityTypeBuilder;
        }

        public IModelBuilder Ignore<TEntity>() where TEntity : class
        {
            modelBuilder.Ignore<TEntity>();
            return this;
        }

        #endregion
    }
}