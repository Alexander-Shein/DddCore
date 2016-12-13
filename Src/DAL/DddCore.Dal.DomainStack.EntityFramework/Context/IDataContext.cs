using System;
using DddCore.Contracts.Dal.DomainStack;
using DddCore.Contracts.Domain.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace DddCore.Dal.DomainStack.EntityFramework.Context
{
    public interface IDataContext : IUnitOfWork, IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SyncEntityState<T>(T entity) where T : class, ICrudState;
    }
}
