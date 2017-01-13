using System;

namespace DddCore.Contracts.Domain.Entities.Model
{
    public interface IVersion
    {
        Byte[] Ts { get; set; }
    }
}