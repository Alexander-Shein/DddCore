using System.Threading.Tasks;
using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.SL.Services.Application.DomainStack.Crud.Async
{
    public interface IDeleteAsync<in TKey>
    {
        /// <summary>
        /// DELETE /cars/{carId} HTTP/1.1.
        /// Deletes entity by key.
        /// </summary>
        /// <param name="key"></param>
        Task<OperationResult> DeleteAsync(TKey key);
    }
}