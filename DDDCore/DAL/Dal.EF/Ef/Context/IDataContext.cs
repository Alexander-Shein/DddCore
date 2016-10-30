using System;
using System.Data.Entity;
using Contracts.Dal.DomainStack;
using Contracts.Domain.Entities.Model;

namespace Dal.DomainStack.Ef.Context
{
    public interface IDataContext : IUnitOfWork, IDisposable
    {
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SyncEntityState<T>(T entity) where T : class, ICrudState;
    }
}
