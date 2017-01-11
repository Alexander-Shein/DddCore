namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IRead<T, in TKey>
    {
        T Read(TKey key, string[] includes = null);
    }
}
