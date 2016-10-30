using System;

namespace Contracts.Domain.Entities.Audit
{
    public interface ICreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}