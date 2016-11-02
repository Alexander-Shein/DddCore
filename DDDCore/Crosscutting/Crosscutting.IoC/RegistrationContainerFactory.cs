using System;
using Contracts.Crosscutting.IoC;
using Contracts.Crosscutting.IoC.Base;
using Crosscutting.Ioc.CastleWindsor;

namespace Crosscutting.Ioc
{
    public class RegistrationContainerFactory : IContainerConfigFactory
    {
        public IContainerConfig Create(ContainerType containerType)
        {
            switch (containerType)
            {
                case ContainerType.CastleWindsor:
                    return new WindsorContainerWrapper();

                default: throw new NotImplementedException();
            }
        }
    }
}