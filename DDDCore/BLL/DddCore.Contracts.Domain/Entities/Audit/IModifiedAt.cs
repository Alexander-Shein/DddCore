using System;

namespace DddCore.Contracts.Domain.Entities.Audit
{
    public interface IModifiedAt
    {
        DateTime ModifiedAt { get; set; }
    }
}