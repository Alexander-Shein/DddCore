using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Dal.DomainStack;
using Contracts.Domain.Entities;
using Contracts.Domain.Entities.Audit;
using Contracts.Domain.Entities.Model;
using Dal.DomainStack.Ef.Context;

namespace Dal.DomainStack.Ef
{
    public class RepositoryBase<T, TKey> : IRepository<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        protected readonly IDataContext dataContext;

        public RepositoryBase(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        #region Public Methods

        public void PersistEntityGraph(T entity)
        {
            SyncObjectGraph(entity);
        }

        public virtual T Read(TKey id)
        {
            return GetDbSet().Find(id);
        }

        public virtual Task<T> ReadAsync(TKey id)
        {
            return GetDbSet().FindAsync(id);
        }

        #endregion

        protected DbSet<T> GetDbSet()
        {
            var set = dataContext.Set<T>();
            return set;
        }

        #region Private Methods

        static void UpdateAuditableInfo(ICrudState entity)
        {
            bool isAdded = entity.CrudState == CrudState.Added;
            bool isModified = isAdded || entity.CrudState == CrudState.Modified;

            if (isAdded)
            {
                AddCreatedDate(entity);
            }

            if (isModified)
            {
                AddModifiedDate(entity);
            }
        }

        static void AddCreatedDate(object entity)
        {
            var model = entity as IModifiedAt;

            if (model != null)
            {
                model.ModifiedAt = DateTime.UtcNow;
            }
        }

        static void AddModifiedDate(object entity)
        {
            var model = entity as ICreatedAt;

            if (model != null)
            {
                model.CreatedAt = DateTime.UtcNow;
            }
        }

        void SyncObjectGraph<TEntity>(TEntity entity, HashSet<object> hashSet = null) where TEntity : class, ICrudState
        {
            if (hashSet == null)
            {
                hashSet = new HashSet<object>();
            }

            if (!hashSet.Add(entity)) return;

            var type = entity.GetType();

            UpdateAuditableInfo(entity);

            // Set tracking state for child collections
            foreach (
                var prop in
                    type.GetProperties()
                        .Where(p => !typeof(MulticastDelegate).IsAssignableFrom(p.PropertyType.BaseType))) //TODO use caching
            {
                var propValue = prop.GetValue(entity, null);

                // Apply changes to 1-1 and M-1 properties
                var trackableRef = propValue as ICrudState;//TODO :use propertyfactory

                if (trackableRef != null)
                {
                    SyncObjectGraph(trackableRef, hashSet);
                    continue;
                }

                // Apply changes to 1-M properties
                var items = propValue as IEnumerable<ICrudState>;//TODO :use propertyfactory
                if (items == null) continue;

                foreach (var item in items.ToList())
                {
                    SyncObjectGraph(item, hashSet); //TODO set depth level
                }
            }

            dataContext.Set(type).Attach(entity);
            dataContext.SyncEntityState(entity);
        }

        #endregion
    }
}
