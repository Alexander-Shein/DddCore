using System;

namespace DddCore.BLL.Domain.Entities.GuidEntities
{
    public abstract class GuidAggregateRootBase : AggregateRootBase<Guid>
    {
        protected GuidAggregateRootBase()
        {
            Id = Guid.NewGuid();
        }
    }
}