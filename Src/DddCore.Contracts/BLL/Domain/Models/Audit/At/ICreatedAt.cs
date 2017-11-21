using System;

namespace DddCore.Contracts.BLL.Domain.Models.Audit.At
{
    public interface ICreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}