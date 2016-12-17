using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DddCore.Contracts.Domain.Entities;
using DddCore.Crosscutting;

namespace DddCore.Domain.Entities
{
    public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
    {
        #region Public Methods

        public string PublicKey { get; set; }

        public byte[] Ts { get; set; }

        public void WalkEntireGraph(Action<IEntity<TKey>> action)
        {
            foreach (var entity in Traverse(this))
            {
                action(entity);
            }
        }

        public void WalkAggregateRootGraph(Action<IEntity<TKey>> action)
        {
            foreach (var entity in Traverse(this, true))
            {
                action(entity);
            }
        }

        #endregion

        #region Private Methods

        IEnumerable<TEntity> Traverse<TEntity>(TEntity entity, bool aggregateRootOnly = false) where TEntity : class, IEntity<TKey>
        {
            var stack = new Stack<TEntity>();
            stack.Push(entity);
            while (stack.Count != 0)
            {
                TEntity item = stack.Pop();
                yield return item;

                var type = item.GetType().GetTypeInfo();

                foreach (var prop in type.GetProperties())
                {
                    var propValue = prop.GetValue(entity, null);

                    var trackableRef = propValue as TEntity;

                    if (trackableRef != null && !SkipAggregateRoot(aggregateRootOnly, trackableRef))
                    {
                        stack.Push(trackableRef);
                        continue;
                    }

                    var entities = propValue as IEnumerable<TEntity>;
                    if (entities.IsNullOrEmpty() || SkipAggregateRoot(aggregateRootOnly, entities.First())) continue;

                    foreach (var element in entities.ToList())
                    {
                        stack.Push(element);
                    }
                }
            }
        }

        bool SkipAggregateRoot(bool aggregateRootOnly, IEntity<TKey> entity)
        {
            if (aggregateRootOnly && entity is IAggregateRootEntity<TKey>) return true;
            return false;
        }

        #endregion
    }
}