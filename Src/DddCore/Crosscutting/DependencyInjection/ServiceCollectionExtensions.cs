using System.Linq;
using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
using DddCore.SL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Crosscutting.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDddCore(this IServiceCollection serviceCollection)
        {
            var modules = AssemblyUtility.GetInstancesOf<IDiModuleInstaller>().ToList();
            modules.Add(new DddCoreDiModuleInstaller());

            new DiBootstrapper()
                .AddConfig(serviceCollection)
                .Bootstrap(modules.ToArray());
        }
    }
}