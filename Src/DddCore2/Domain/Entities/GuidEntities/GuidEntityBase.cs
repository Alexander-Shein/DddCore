using System;

namespace DddCore.Domain.Entities.GuidEntities
{
    public abstract class GuidEntityBase : EntityBase<Guid>
    {
        protected GuidEntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}