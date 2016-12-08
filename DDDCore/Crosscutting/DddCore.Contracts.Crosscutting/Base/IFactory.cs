namespace DddCore.Contracts.Crosscutting.Base
{
    public interface IFactory<out T, in TType>
    {
        T Create(TType type);
    }
}