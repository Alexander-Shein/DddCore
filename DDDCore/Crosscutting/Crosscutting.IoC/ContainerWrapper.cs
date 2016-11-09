using System;
using Contracts.Crosscutting.Ioc;

namespace Crosscutting.Ioc
{
    public class ContainerWrapper : IContainer
    {
        #region Public Methods

        public T Resolve<T>() where T : class 
        {
            return ContainerHolder.Container.Resolve<T>();
        }

        public T Resolve<T>(string name) where T : class
        {
            return ContainerHolder.Container.Resolve<T>(name);
        }

        public object Resolve(Type type)
        {
            return ContainerHolder.Container.Resolve(type);
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

        #endregion
    }
}