using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddCore.DAL.DomainStack.EntityFramework.Mapping
{
    public interface IModelBuilder
    {
        EntityTypeBuilder<TEntity> Entity<TEntity>() where TEntity : class;
        EntityTypeBuilder<TEntity> Entity<TEntity>(Action<EntityTypeBuilder<TEntity>> buildAction) where TEntity : class;
        IModelBuilder Ignore<TEntity>() where TEntity : class;
    }
}