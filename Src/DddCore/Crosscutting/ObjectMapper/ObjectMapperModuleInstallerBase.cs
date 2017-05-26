using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.Crosscutting.ObjectMapper.Base;

namespace DddCore.Crosscutting.ObjectMapper
{
    public abstract class ObjectMapperModuleInstallerBase : IObjectMapperModuleInstaller
    {
        protected IObjectMapperConfig Config;

        protected abstract void FromDtoToView();
        protected abstract void FromDomainToView();
        protected abstract void FromInputToDomain();

        public void Install(IObjectMapperConfig config)
        {
            Config = config;

            FromDtoToView();
            FromDomainToView();
            FromInputToDomain();
        }
    }
}