using System;

namespace Contracts.Domain.Entities.Audit
{
    public interface IModifiedAt
    {
        DateTime ModifiedAt { get; set; }
    }
}