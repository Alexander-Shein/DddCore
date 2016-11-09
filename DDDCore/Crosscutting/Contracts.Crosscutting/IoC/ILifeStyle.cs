namespace Contracts.Crosscutting.Ioc
{
    public interface ILifeStyle
    {
        void Transient();
        void PerWebRequest();
        void Singleton();
    }
}