using System;

namespace DddCore.Contracts.BLL.Domain.Models.Audit.At
{
    public interface IModifiedAt
    {
        DateTime ModifiedAt { get; set; }
    }
}