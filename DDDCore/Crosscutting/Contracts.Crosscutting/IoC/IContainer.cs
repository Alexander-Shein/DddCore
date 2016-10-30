namespace Contracts.Crosscutting.IoC
{
    public interface IContainer
    {
        T Resolve<T>() where T : class;
        T Resolve<T>(string name) where T : class;
        T[] ResolveAll<T>() where T : class;
    }
}