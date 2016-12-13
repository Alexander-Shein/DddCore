using DddCore.Contracts.Crosscutting.Base;
using Microsoft.EntityFrameworkCore;

namespace DddCore.Dal.DomainStack.EntityFramework.Mapping
{
    public interface IMappingModule : IModule<ModelBuilder>
    {
    }
}