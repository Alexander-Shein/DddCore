using System;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace DddCore.DAL.DomainStack.EntityFramework.Context
{
    public interface IDataContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SyncEntityState<T>(T entity) where T : class, ICrudState;
    }
}
