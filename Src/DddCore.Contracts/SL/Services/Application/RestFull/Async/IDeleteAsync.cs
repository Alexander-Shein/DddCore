using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.SL.Services.Application.RestFull.Async
{
    public interface IDeleteAsync<in TKey>
    {
        /// <summary>
        /// DELETE /cars/{carId} HTTP/1.1.
        /// Deletes entity by key.
        /// </summary>
        /// <param name="key"></param>
        Task<Result> DeleteAsync(TKey key);
    }
}