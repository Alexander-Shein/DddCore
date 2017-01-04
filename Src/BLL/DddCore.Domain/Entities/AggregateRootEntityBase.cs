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

        IEnumerable<IEntity<TKey>> Traverse(IEntity<TKey> entity, bool aggregateRootOnly = false)
        {
            var stack = new Stack<IEntity<TKey>>();
            stack.Push(entity);
            while (stack.Count != 0)
            {
                IEntity<TKey> item = stack.Pop();
                yield return item;

                var type = item.GetType();

                foreach (var prop in type.GetProperties())
                {
                    var propValue = prop.GetValue(item, null);

                    if (propValue is IEntity<TKey> trackableRef && !SkipAggregateRoot(aggregateRootOnly, trackableRef))
                    {
                        stack.Push(trackableRef);
                        continue;
                    }

                    var entities = propValue as IEnumerable<IEntity<TKey>>;
                    if (entities.IsNullOrEmpty() || SkipAggregateRoot(aggregateRootOnly, entities.First())) continue;

                    foreach (var element in entities)
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