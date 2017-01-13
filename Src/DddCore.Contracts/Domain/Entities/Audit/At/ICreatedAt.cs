using System;

namespace DddCore.Contracts.Domain.Entities.Audit.At
{
    public interface ICreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}