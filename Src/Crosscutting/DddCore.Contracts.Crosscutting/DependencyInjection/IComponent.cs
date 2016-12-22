namespace DddCore.Contracts.Crosscutting.DependencyInjection
{
    public interface IComponent
    {
        ILifeStyle LifeStyle { get; }
    }
}