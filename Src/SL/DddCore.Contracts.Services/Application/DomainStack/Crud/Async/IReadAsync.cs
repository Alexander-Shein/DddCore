using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface IReadAsync<T, in TKey>
    {
        Task<T> ReadAsync(TKey key, string[] includes = null);
    }
}