using System;

namespace DddCore.Contracts.BLL.Domain.Entities.Audit.At
{
    public interface IModifiedAt
    {
        DateTime ModifiedAt { get; set; }
    }
}