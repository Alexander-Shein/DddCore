using System;

namespace DddCore.Contracts.BLL.Domain.Entities.Model
{
    public interface IEntityGraph<TKey>
    {
        void WalkGraph(Action<IEntity<TKey>> action, GraphDepth graphDepth = GraphDepth.AggregateRoot);
    }
}