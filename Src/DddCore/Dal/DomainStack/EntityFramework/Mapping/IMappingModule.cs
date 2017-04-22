using DddCore.Contracts.Crosscutting.Base;

namespace DddCore.DAL.DomainStack.EntityFramework.Mapping
{
    public interface IMappingModule : IModuleInstaller<IModelBuilder>
    {
    }
}