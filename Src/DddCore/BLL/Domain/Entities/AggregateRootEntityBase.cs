using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.Model;
using DddCore.Contracts.BLL.Errors;
using DddCore.Crosscutting;

namespace DddCore.BLL.Domain.Entities
{
    public abstract class AggregateRootEntityBase<TKey> : EntityBase<TKey>, IAggregateRootEntity<TKey>
    {
        #region Public Methods

        public void WalkGraph(Action<IEntity<TKey>> action, GraphDepth graphDepth = GraphDepth.AggregateRoot)
        {
            switch (graphDepth)
            {
                case GraphDepth.Itself:
                {
                    action(this);
                    return;
                }
                case GraphDepth.AggregateRoot:
                {
                    Traverse(this, true).Do(action);
                    return;
                }
                case GraphDepth.Entire:
                {
                    Traverse(this).Do(action);
                        return;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(graphDepth), graphDepth, null);
            }
        }

        public void RaiseEvents(GraphDepth graphDepth = GraphDepth.AggregateRoot)
        {
            WalkGraph(entity => entity.RaiseEvents(), graphDepth);
        }

        public Task<OperationResult> ValidateAsync(GraphDepth graphDepth = GraphDepth.AggregateRoot)
        {
            return Task.FromResult(Validate(graphDepth));
        }

        public OperationResult Validate(GraphDepth graphDepth = GraphDepth.AggregateRoot)
        {
            var result = new OperationResult();

            WalkGraph(entity =>
            {
                var nodeResult = entity.Validate();

                if (!nodeResult.IsNotSucceed) return;

                foreach (var error in nodeResult.Errors)
                {
                    result.Errors.Add(error);
                }
            }, graphDepth);

            return result;
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
