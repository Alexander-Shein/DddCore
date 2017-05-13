using System;
using System.Reflection;
using DddCore.Contracts.BLL.Domain.Entities;
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
            public const string Events = "Events";
            public const string DomainEventDispatcher = "DomainEventDispatcher";
            public const string BusinessRulesValidatorFactory = "BusinessRulesValidatorFactory";
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

            if (typeof(IEntity<>).IsAssignableFromGenericType(type))
            {
                entityTypeBuilder.Ignore(FieldName.Events);
                entityTypeBuilder.Ignore(FieldName.DomainEventDispatcher);
                entityTypeBuilder.Ignore(FieldName.BusinessRulesValidatorFactory);
            }

            return entityTypeBuilder;
        }

        public EntityTypeBuilder<TEntity> Entity<TEntity>(Action<EntityTypeBuilder<TEntity>> buildAction) where TEntity : class
        {
            var builder = Entity<TEntity>();
            buildAction(builder);
            return builder;
        }

        public IModelBuilder Ignore<TEntity>() where TEntity : class
        {
            modelBuilder.Ignore<TEntity>();
            return this;
        }

        #endregion
    }
}