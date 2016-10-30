using System;

namespace Domain.Entities.GuidEntities
{
    public abstract class GuidAggregateRootEntityBase : AggregateRootEntityBase<Guid>
    {
        protected GuidAggregateRootEntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}