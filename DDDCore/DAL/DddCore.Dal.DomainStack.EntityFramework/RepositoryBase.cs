using System;
using System.Threading.Tasks;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.Dal.DomainStack;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Entities.Audit;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Dal.DomainStack.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace DddCore.Dal.DomainStack.EntityFramework
{
    public abstract class RepositoryBase<T, TKey> : IRepository<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        #region Private Members

        readonly IUserContext<TKey> userContext;
        DateTime now;

        #endregion

        protected readonly IDataContext DataContext;

        protected RepositoryBase(IDataContext dataContext, IUserContext<TKey> userContext)
        {
            DataContext = dataContext;
            this.userContext = userContext;
        }

        #region Public Methods

        public void PersistAggregateRoot(T entity)
        {
            now = DateTime.UtcNow;

            entity.WalkAggregateRootGraph(node =>
            {
                UpdateAuditableInfo(node);
                DataContext.SyncEntityState(node);
                node.CrudState = CrudState.Unchanged;
            });
        }

        public abstract Task<T> ReadAggregateRootAsync(TKey key);

        #endregion

        protected DbSet<T> GetDbSet()
        {
            var set = DataContext.Set<T>();
            return set;
        }

        #region Private Methods

        void UpdateAuditableInfo(ICrudState entity)
        {
            bool isAdded = entity.CrudState == CrudState.Added;
            bool isModified = isAdded || entity.CrudState == CrudState.Modified;

            if (isAdded)
            {
                AddCreatedAt(entity);
                AddCreatedBy(entity);
            }

            if (isModified)
            {
                AddModifiedAt(entity);
                AddModifiedBy(entity);
            }
        }

        void AddModifiedBy(object entity)
        {
            var model = entity as IModifiedBy<TKey>;

            if (model != null)
            {
                model.ModifiedBy = userContext.Id;
            }
        }

        void AddCreatedBy(object entity)
        {
            var model = entity as ICreatedBy<TKey>;

            if (model != null)
            {
                model.CreatedBy = userContext.Id;
            }
        }

        void AddCreatedAt(object entity)
        {
            var model = entity as ICreatedAt;

            if (model != null)
            {
                model.CreatedAt = now;
            }
        }

        void AddModifiedAt(object entity)
        {
            var model = entity as IModifiedAt;

            if (model != null)
            {
                model.ModifiedAt = now;
            }
        }

        #endregion
    }
}
