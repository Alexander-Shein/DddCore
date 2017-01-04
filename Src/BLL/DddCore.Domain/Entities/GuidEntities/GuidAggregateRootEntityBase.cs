using System;

namespace DddCore.Domain.Entities.GuidEntities
{
    public abstract class GuidAggregateRootEntityBase : AggregateRootEntityBase<Guid>
    {
        protected GuidAggregateRootEntityBase()
        {
            Id = Guid.NewGuid();
            PublicKey = Id.ToString();
        }
    }
}