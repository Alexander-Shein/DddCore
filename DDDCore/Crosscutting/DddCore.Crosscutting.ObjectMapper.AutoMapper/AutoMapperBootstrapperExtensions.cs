using DddCore.Contracts.Crosscutting.ObjectMapper.Base;

namespace DddCore.Crosscutting.ObjectMapper.AutoMapper
{
    public static class AutoMapperBootstrapperExtensions
    {
        public static IObjectMapperBootstrapper AddAutoMapperConfig(this IObjectMapperBootstrapper bootstrapper)
        {
            bootstrapper.AddConfig(new AutoMapperObjectMapperConfig());
            return bootstrapper;
        }
    }
}