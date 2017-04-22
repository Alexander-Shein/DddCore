using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Crosscutting;

namespace DddCore.BLL.Domain.Entities
{
    public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
    {
        #region Public Methods

        public void WalkEntireGraph(Action<IEntity<TKey>> action)
        {
            Traverse(this).Do(action);
        }

        public void WalkAggregateRootGraph(Action<IEntity<TKey>> action)
        {
            Traverse(this, true).Do(action);
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
            return aggregateRootOnly && entity is IAggregateRootEntity<TKey>;
        }

        #endregion
    }
}