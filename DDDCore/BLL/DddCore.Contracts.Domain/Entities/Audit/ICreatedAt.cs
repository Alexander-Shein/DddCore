using System;

namespace DddCore.Contracts.Domain.Entities.Audit
{
    public interface ICreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}