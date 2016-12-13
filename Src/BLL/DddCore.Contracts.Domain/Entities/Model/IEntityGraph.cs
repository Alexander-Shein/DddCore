using System;

namespace DddCore.Contracts.Domain.Entities.Model
{
    public interface IEntityGraph
    {
        void WalkEntireGraph(Action<ICrudState> action);
        void WalkAggregateRootGraph(Action<ICrudState> action);
    }
}