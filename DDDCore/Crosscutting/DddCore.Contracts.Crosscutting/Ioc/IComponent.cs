namespace DddCore.Contracts.Crosscutting.Ioc
{
    public interface IComponent
    {
        IComponent Named(string name);
        ILifeStyle LifeStyle { get; }
    }
}