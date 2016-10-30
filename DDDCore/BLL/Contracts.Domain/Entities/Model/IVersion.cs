using System;

namespace Contracts.Domain.Entities.Model
{
    public interface IVersion
    {
        Byte[] Ts { get; set; }
    }
}