using System;

namespace Contracts.Crosscutting.IoC
{
    public interface IContainer
    {
        T Resolve<T>() where T : class;
        T Resolve<T>(string name) where T : class;

        object Resolve(Type type);
        T[] ResolveAll<T>() where T : class;
    }
}