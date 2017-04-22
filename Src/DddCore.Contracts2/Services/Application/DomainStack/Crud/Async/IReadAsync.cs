using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface IReadAsync<TViewModel, in TKey>
    {
        /// <summary>
        /// GET /cars/{carId} HTTP/1.1.
        /// Reads entity by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includes">Includes can contain additional information that we need to return.</param>
        /// <returns></returns>
        Task<TViewModel> ReadAsync(TKey key, string[] includes = null);
    }
}