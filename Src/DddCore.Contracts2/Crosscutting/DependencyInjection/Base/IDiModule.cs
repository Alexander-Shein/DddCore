using DddCore.Contracts.Crosscutting.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Contracts.Crosscutting.DependencyInjection.Base
{
    public interface IDiModule : IModule<IServiceCollection>
    {
    }
}