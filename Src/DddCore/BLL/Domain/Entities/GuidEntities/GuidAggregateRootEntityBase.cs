using System;

namespace DddCore.BLL.Domain.Entities.GuidEntities
{
    public abstract class GuidAggregateRootEntityBase : AggregateRootEntityBase<Guid>
    {
        protected GuidAggregateRootEntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}