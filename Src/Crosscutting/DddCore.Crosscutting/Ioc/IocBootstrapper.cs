using DddCore.Contracts.Crosscutting.Ioc;
using DddCore.Contracts.Crosscutting.Ioc.Base;
using DddCore.Crosscutting.Base;

namespace DddCore.Crosscutting.Ioc
{
    public class IocBootstrapper : BootstrapperBase<IContainer, IContainerConfig, IIocModule>, IIocBootstrapper
    {
    }
}