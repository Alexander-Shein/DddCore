using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface IDeleteAsync<in TKey>
    {
        /// <summary>
        /// DELETE /cars/{carId} HTTP/1.1.
        /// Deletes entity by key.
        /// </summary>
        /// <param name="key"></param>
        Task DeleteAsync(TKey key);
    }
}