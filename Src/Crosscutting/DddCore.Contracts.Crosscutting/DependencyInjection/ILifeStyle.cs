namespace DddCore.Contracts.Crosscutting.DependencyInjection
{
    public interface ILifeStyle
    {
        void Transient();
        void PerWebRequest();
        void Singleton();
    }
}