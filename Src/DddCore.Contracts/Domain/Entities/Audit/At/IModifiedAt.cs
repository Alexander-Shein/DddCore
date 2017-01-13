using System;

namespace DddCore.Contracts.Domain.Entities.Audit.At
{
    public interface IModifiedAt
    {
        DateTime ModifiedAt { get; set; }
    }
}