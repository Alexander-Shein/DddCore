namespace Contracts.Crosscutting
{
    public interface IShallowCloneable<out T>
    {
        T ShallowCopy();
    }
}
