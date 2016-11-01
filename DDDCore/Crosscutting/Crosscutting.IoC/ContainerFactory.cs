using System;
using Contracts.Crosscutting.IoC;
using Crosscutting.Ioc.CastleWindsor;

namespace Crosscutting.Ioc
{
    public class ContainerFactory
    {
        public IRegistrationContainer Create(ContainerType containerType)
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