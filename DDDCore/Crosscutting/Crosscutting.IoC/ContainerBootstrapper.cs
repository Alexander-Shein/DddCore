using System.Linq;
using Contracts.Crosscutting.IoC;
using Crosscutting.Infrastructure.Configuration;

namespace Crosscutting.Ioc
{
    public class ContainerBootstrapper : IContainerBootstrapper
    {
        #region Public Methods

        public void Bootstrap(ContainerType containerType)
        {
            var modules =
                AssemblyUtility
                    .GetInstances<IIocModule>()
                    .ToArray();

            Bootstrap(containerType, modules);
        }

        public void Bootstrap(ContainerType containerType, params IIocModule[] iocModules)
        {
            IRegistrationContainer container =
                ContainerHolder.Container == null
                ? new ContainerFactory().Create(containerType)
                : (IRegistrationContainer) ContainerHolder.Container;

            if (iocModules != null)
            {
                foreach (var iocModule in iocModules)
                {
                    iocModule.Install(container);
                }
            }

            ContainerHolder.Container = container;
        }

        #endregion
    }
}
