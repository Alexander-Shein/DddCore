using System;
using Contracts.Crosscutting.IoC;

namespace Crosscutting.IoC
{
    public class ContainerWrapper : IContainer
    {
        public T Resolve<T>() where T : class 
        {
            return ContainerHolder.Container.Resolve<T>();
        }

        public T Resolve<T>(string name) where T : class
        {
            return ContainerHolder.Container.Resolve<T>(name);
        }

        public T[] ResolveAll<T>() where T : class
        {
            try
            {
                return ContainerHolder.Container.ResolveAll<T>();
            }
            catch (Exception)
            {
                return new T[0];
            }
        }
    }
}