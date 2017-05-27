using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Crosscutting.ObjectMapper.AutoMapper
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mapper = new ObjectMapperBootstrapper()
                .AddAutoMapperConfig()
                .Bootstrap();

            serviceCollection.AddSingleton(mapper);
        }
    }
}