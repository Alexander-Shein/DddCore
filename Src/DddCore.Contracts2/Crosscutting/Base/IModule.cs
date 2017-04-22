namespace DddCore.Contracts.Crosscutting.Base
{
    public interface IModule<in T>
    {
        void Install(T config);
    }
}