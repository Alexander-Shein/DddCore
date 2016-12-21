namespace DddCore.Contracts.Crosscutting.DependencyInjection
{
    public interface IComponent
    {
        IComponent Named(string name);
        ILifeStyle LifeStyle { get; }
    }
}