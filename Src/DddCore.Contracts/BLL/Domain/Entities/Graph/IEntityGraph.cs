using System;

namespace DddCore.Contracts.BLL.Domain.Entities.Graph
{
    public interface IEntityGraph<TKey>
    {
        void WalkGraph(Action<IEntity<TKey>> action, GraphDepth graphDepth = GraphDepth.AggregateRoot);
    }
}