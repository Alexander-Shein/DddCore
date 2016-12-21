using System;
using DddCore.Contracts.Crosscutting.Base;

namespace DddCore.Contracts.Crosscutting.DependencyInjection.Base
{
    public interface IDiBootstrapper : IBootstrapper<IServiceProvider, IContainerConfig, IDiModule>
    {
    }
}