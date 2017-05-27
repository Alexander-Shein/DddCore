using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Graph;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.BLL.Errors;
using DddCore.Crosscutting;

namespace DddCore.BLL.Domain.Entities
{
    public abstract class AggregateRootBase<TKey> : EntityBase<TKey>, IAggregateRoot<TKey>
    {
        #region Public Methods

        public void WalkGraph(Action<IEntity<TKey>> action, GraphDepth graphDepth = GraphDepth.AggregateRoot)
        {
            Guard.ThrowIfNull(action, nameof(action));

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

        public OperationResult RaiseEvents(IDomainEventDispatcher eventDispatcher, GraphDepth graphDepth)
        {
            Guard.ThrowIfNull(eventDispatcher, nameof(eventDispatcher));

            var result = new OperationResult();

            WalkGraph(entity =>
            {
                var nodeResult = entity.RaiseEvents(eventDispatcher);

                if (nodeResult.IsSucceed) return;

                foreach (var error in nodeResult.Errors)
                {
                    result.Errors.Add(error);
                }
            }, graphDepth);

            return result;
        }

        public Task<OperationResult> ValidateAsync(IBusinessRulesValidatorFactory factory, GraphDepth graphDepth)
        {
            return Task.FromResult(Validate(factory, graphDepth));
        }

        public OperationResult Validate(IBusinessRulesValidatorFactory factory, GraphDepth graphDepth)
        {
            Guard.ThrowIfNull(factory, nameof(factory));

            var result = new OperationResult();

            WalkGraph(entity =>
            {
                var nodeResult = entity.Validate(factory);

                if (nodeResult.IsSucceed) return;

                foreach (var error in nodeResult.Errors)
                {
                    result.Errors.Add(error);
                }
            }, graphDepth);

            return result;
        }

        public override OperationResult RaiseEvents(IDomainEventDispatcher eventDispatcher)
        {
            return RaiseEvents(eventDispatcher, GraphDepth.AggregateRoot);
        }

        public override async Task<OperationResult> ValidateAsync(IBusinessRulesValidatorFactory factory)
        {
            return await ValidateAsync(factory, GraphDepth.AggregateRoot);
        }

        public override OperationResult Validate(IBusinessRulesValidatorFactory factory)
        {
            return Validate(factory, GraphDepth.AggregateRoot);
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
            return aggregateRootOnly && entity is IAggregateRoot<TKey>;
        }

        #endregion
    }
}