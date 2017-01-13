using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Crosscutting.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDddCore(this IServiceCollection serviceCollection)
        {
            new DiBootstrapper()
                .AddConfig(serviceCollection)
                .Bootstrap();
        }
    }
}
