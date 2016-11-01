namespace Contracts.Crosscutting.IoC
{
    public interface IComponent
    {
        ILifeStyle LifeStyle { get; }
    }
}