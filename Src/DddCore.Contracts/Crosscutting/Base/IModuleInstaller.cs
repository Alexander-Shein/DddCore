namespace DddCore.Contracts.Crosscutting.Base
{
    /// <summary>
    /// Examples: When you need to add dependencies to IoC container or need to config object mapper use Modules approach. Create a module for feature and configure your models in the module. Then bootstrapper will scan assemplies for Modules and call Install method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IModuleInstaller<in T>
    {
        void Install(T config);
    }
}