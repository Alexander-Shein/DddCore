using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DddCore.Contracts.Domain.Entities;
using DddCore.Contracts.Domain.Entities.Model;

namespace DddCore.Domain.Entities
{
    public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
    {
        #region Public Methods

        public string PublicKey { get; set; }

        public byte[] Ts { get; set; }

        public void WalkEntireGraph(Action<ICrudState> action)
        {
            WalkObjectGraph(this, action);
        }

        public void WalkAggregateRootGraph(Action<ICrudState> action)
        {
            WalkObjectGraph(this, action, null, true);
        }

        #endregion

        #region Private Methods

        void WalkObjectGraph<TEntity>(TEntity entity, Action<ICrudState> action, HashSet<object> hashSet = null, bool currentAggregateRootOnly = false) where TEntity : class, ICrudState
        {
            if (hashSet == null)
            {
                hashSet = new HashSet<object>();
            }
            else if (currentAggregateRootOnly)
            {
                if (entity is IAggregateRootEntity<TKey>)
                {
                    hashSet.Add(entity);
                    return;
                }
            }

            if (!hashSet.Add(entity)) return;

            var type = entity.GetType();

            // Set tracking state for child collections
            foreach (
                var prop in
                    type.GetRuntimeProperties()) //TODO use caching
            {
                var propValue = prop.GetValue(entity, null);

                // Apply changes to 1-1 and M-1 properties
                var trackableRef = propValue as ICrudState;//TODO :use propertyfactory

                if (trackableRef != null)
                {
                    WalkObjectGraph(trackableRef, action, hashSet);
                    continue;
                }

                // Apply changes to 1-M properties
                var items = propValue as IEnumerable<ICrudState>;//TODO :use propertyfactory
                if (items == null) continue;

                foreach (var item in items.ToList())
                {
                    WalkObjectGraph(item, action, hashSet); //TODO set depth level
                }
            }

            action(entity);
        }

        #endregion
    }
}