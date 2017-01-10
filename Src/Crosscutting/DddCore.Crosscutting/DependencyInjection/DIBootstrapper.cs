using System;
using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
using DddCore.Crosscutting.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Crosscutting.DependencyInjection
{
    public class DiBootstrapper : BootstrapperBase<IServiceProvider, IServiceCollection, IDiModule>, IDiBootstrapper
    {
        protected override IServiceProvider GetInstance(IServiceCollection serviceCollection)
        {
            return serviceCollection.BuildServiceProvider(true);
        }
    }
}