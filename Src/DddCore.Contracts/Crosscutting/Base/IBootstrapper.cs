namespace DddCore.Contracts.Crosscutting.Base
{
    /// <summary>
    /// Exampes: When you need to setup object mapper or add objects to IoC container use IBootstrapper that scans assemplies for specified modules and executes all module.Install methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TConfig"></typeparam>
    /// <typeparam name="TModule"></typeparam>
    public interface IBootstrapper<out T, in TConfig, in TModule> where TModule : IModuleInstaller<TConfig>
    {
        IBootstrapper<T, TConfig, TModule> AddConfig(TConfig config);
        T Bootstrap();
        T Bootstrap(params TModule[] modules);
    }
}