using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface ICreateChildAsync<TViewModel, in TParrentKey, in TInputModel>
    {
        /// <summary>
        /// Example: POST /cars/{carId}/wheels HTTP/1.1.
        /// When you need to create an item in child collection you can use this interface.
        /// But we don't need UpdateChild/DeleteChild/ReadChild because we already have an id and can use next short urls without /cars/{carId} prefix: GET/DELETE/PUT /wheels/55.
        /// </summary>
        /// <param name="key">This is a parrent item key</param>
        /// <param name="im">InputModel has no Id property because when we send request to create new object we don't know id.</param>
        /// <returns>ViewModel contains generated Id property.</returns>
        Task<TViewModel> CreateChildAsync(TParrentKey key, TInputModel im);
    }
}