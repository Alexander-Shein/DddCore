using System;

namespace DddCore.Contracts.BLL.Domain.Entities.Audit.At
{
    public interface ICreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}