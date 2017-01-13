using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface IDeleteAsync<in TKey>
    {
        Task DeleteAsync(TKey key);
    }
}