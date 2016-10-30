using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.Crosscutting.UserContext;
using Contracts.Dal.DomainStack;
using Contracts.Domain.Entities;
using Contracts.Domain.Entities.Audit;
using Contracts.Domain.Entities.Model;
using Dal.DomainStack.Ef.Context;

namespace Dal.DomainStack.Ef
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeRange<T, TProperty>(this IQueryable<T> source, params Expression<Func<T, TProperty>>[] paths)
        {
            if (paths == null)
                throw new NullReferenceException();

            return paths.Aggregate(source, (current, path) => current.Include(path));
        }
    }

    public class RepositoryBase<T, TKey> : IRepository<T, TKey> where T : class, IAggregateRootEntity<TKey>
    {
        protected readonly IDataContext dataContext;
        readonly IUserContext<TKey> userContext;

        public RepositoryBase(IDataContext dataContext, IUserContext<TKey> userContext)
        {
            this.dataContext = dataContext;
            this.userContext = userContext;
        }

        #region Public Methods

        public void PersistEntityGraph(T entity)
        {
            SyncObjectGraph(entity);
        }

        public virtual async Task<T> ReadAsync(TKey id, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null) return await GetDbSet().FindAsync(id);

            return await GetDbSet()
                .IncludeRange(includes)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        #endregion

        protected DbSet<T> GetDbSet()
        {
            var set = dataContext.Set<T>();
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
            var model = entity as IModifiedAt;

            if (model != null)
            {
                model.ModifiedAt = DateTime.UtcNow;
            }
        }

        void AddModifiedAt(object entity)
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

            dataContext.SyncEntityState(entity);
        }

        #endregion
    }
}
