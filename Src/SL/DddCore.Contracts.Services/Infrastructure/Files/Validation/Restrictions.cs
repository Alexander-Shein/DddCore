using System;

namespace DddCore.Contracts.Services.Infrastructure.Files.Validation
{
    [Flags]
    public enum Restrictions
    {
        Image = 1,
        Pdf = 2,
        MaxSize50Mb = 4
    }
}