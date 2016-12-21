using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Crosscutting.Ioc.MicrosoftDependencyInjection
{
    public static class DiBootstrapperExtensions
    {
        public static IDiBootstrapper AddMicrosoftDependencyInjection(this IDiBootstrapper diBootstrapper, IServiceCollection serviceCollection)
        {
            diBootstrapper.AddConfig(new ContainerConfig(serviceCollection));
            return diBootstrapper;
        }
    }
}