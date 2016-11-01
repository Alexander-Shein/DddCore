namespace Contracts.Crosscutting.IoC
{
    public interface ILifeStyle
    {
        void Transient();
        void PerWebRequest();
        void Singleton();
    }
}