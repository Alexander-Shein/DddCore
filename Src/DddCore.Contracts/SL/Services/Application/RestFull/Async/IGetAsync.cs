using System.Threading.Tasks;

namespace DddCore.Contracts.SL.Services.Application.RestFull.Async
{
    public interface IGetAsync<TViewModel, in TKey>
    {
        /// <summary>
        /// GET /cars/{carId} HTTP/1.1.
        /// Reads entity by key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includes">Includes can contain additional information that we need to return.</param>
        /// <param name="extends"></param>
        /// <returns></returns>
        Task<TViewModel> GetAsync(TKey key, string[] includes = null, string[] extends = null);
    }
}