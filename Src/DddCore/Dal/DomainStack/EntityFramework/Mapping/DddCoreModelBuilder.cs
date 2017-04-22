using System.Reflection;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Crosscutting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddCore.DAL.DomainStack.EntityFramework.Mapping
{
    public class DddCoreModelBuilder : IModelBuilder
    {
        #region Private Members

        readonly ModelBuilder modelBuilder;

        private static class FieldName
        {
            public const string Id = "Id";
            public const string CrudState = "CrudState";
        }

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

            if (typeof(IIdentity<>).IsAssignableFromGenericType(type))
            {
                entityTypeBuilder.HasKey(FieldName.Id);
            }

            if (typeof(ICrudState).IsAssignableFrom(type))
            {
                entityTypeBuilder.Ignore(FieldName.CrudState);
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