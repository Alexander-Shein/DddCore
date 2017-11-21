using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Services;

namespace DddCore.Contracts.SL.Services.Application.RestFull.Async
{
    public interface IPostAsync<TViewModel, in TInputModel>
    {
        /// <summary>
        /// Example: POST /cars/ HTTP/1.1.
        /// Creates a new entity by InputModel.
        /// </summary>
        /// <param name="im">InputModel has no Id property because when we send request to create new object we don't know id.</param>
        /// <returns>ViewModel contains generated Id property.</returns>
        Task<(TViewModel Vm, Result OperationResult)> PostAsync(TInputModel im);
    }
}
