using System;
using DddCore.Contracts.Crosscutting.Ioc;
using DddCore.Contracts.Crosscutting.Ioc.Base;
using DddCore.Crosscutting.Base;

namespace DddCore.Crosscutting.Ioc
{
    public class IocBootstrapper : BootstrapperBase<IServiceProvider, IContainerConfig, IIocModule>, IIocBootstrapper
    {
        protected override IServiceProvider GetInstance(IContainerConfig config)
        {
            return config.BuildContainer();
        }
    }
}