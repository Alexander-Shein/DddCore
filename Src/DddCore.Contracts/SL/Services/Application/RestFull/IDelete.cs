using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.SL.Services.Application.RestFull
{
    public interface IDelete<in TKey>
    {
        /// <summary>
        /// DELETE /cars/{carId} HTTP/1.1.
        /// Deletes entity by key.
        /// </summary>
        /// <param name="key"></param>
        Result Delete(TKey key);
    }
}
