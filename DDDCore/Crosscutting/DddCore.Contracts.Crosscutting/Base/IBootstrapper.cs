namespace DddCore.Contracts.Crosscutting.Base
{
    public interface IBootstrapper<out T, in TType, in TModule>
    {
        T Bootstrap(TType type);
        T Bootstrap(TType type, params TModule[] modules);
    }
}