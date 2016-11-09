using System.Linq;
using Contracts.Crosscutting.Ioc;
using Contracts.Crosscutting.Ioc.Base;
using Crosscutting.Infrastructure.Configuration;

namespace Crosscutting.Ioc
{
    public class ContainerBootstrapper : IContainerBootstrapper
    {
        #region Public Methods

        public IContainer Bootstrap(ContainerType containerType)
        {
            var modules =
                AssemblyUtility
                    .GetInstances<IIocModule>()
                    .ToArray();

            return Bootstrap(containerType, modules);
        }

        public IContainer Bootstrap(ContainerType containerType, params IIocModule[] iocModules)
        {
            IContainerConfig container =
                ContainerHolder.Container == null
                ? new RegistrationContainerFactory().Create(containerType)
                : (IContainerConfig) ContainerHolder.Container;

            if (iocModules != null)
            {
                foreach (var iocModule in iocModules)
                {
                    iocModule.Install(container);
                }
            }

            ContainerHolder.Container = container;
            return container;
        }

        #endregion
    }
}
