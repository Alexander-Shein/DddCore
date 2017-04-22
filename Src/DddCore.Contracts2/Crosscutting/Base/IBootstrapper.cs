namespace DddCore.Contracts.Crosscutting.Base
{
    public interface IBootstrapper<out T, in TConfig, in TModule> where TModule : IModule<TConfig>
    {
        IBootstrapper<T, TConfig, TModule> AddConfig(TConfig config);
        T Bootstrap();
        T Bootstrap(params TModule[] modules);
    }
}