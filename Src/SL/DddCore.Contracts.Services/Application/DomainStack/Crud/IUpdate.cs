using System;
using System.Collections.Generic;
using System.Text;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IUpdate<TVm, in TKey, in TIm>
    {
        TVm Update(TKey key, TIm model);
    }
}
