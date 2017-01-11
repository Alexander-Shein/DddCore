namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IDelete<in TKey>
    {
        void Delete(TKey key);
    }
}
