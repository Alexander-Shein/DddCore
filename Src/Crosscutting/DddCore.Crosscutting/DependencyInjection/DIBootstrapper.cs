using System;
using DddCore.Contracts.Crosscutting.DependencyInjection;
using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
using DddCore.Crosscutting.Base;

namespace DddCore.Crosscutting.DependencyInjection
{
    public class DiBootstrapper : BootstrapperBase<IServiceProvider, IContainerConfig, IDiModule>, IDiBootstrapper
    {
        protected override IServiceProvider GetInstance(IContainerConfig config)
        {
            return config.BuildContainer();
        }
    }
}