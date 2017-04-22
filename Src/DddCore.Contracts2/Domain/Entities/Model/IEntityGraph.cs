using System;

namespace DddCore.Contracts.Domain.Entities.Model
{
    public interface IEntityGraph<TKey>
    {
        void WalkEntireGraph(Action<IEntity<TKey>> action);
        void WalkAggregateRootGraph(Action<IEntity<TKey>> action);
    }
}