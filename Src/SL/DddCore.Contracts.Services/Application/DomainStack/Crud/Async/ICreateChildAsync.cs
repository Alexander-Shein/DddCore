using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface ICreateChildAsync<TViewModel, in TParrentKey, in TInputModel>
    {
        /// <summary>
        /// It's for creating a child item in the dependent collection.
        /// For example: POST /cars/34/wheel. For this case we can use ICreateChild.
        /// But we don't need UpdateChild/DeleteChild/ReadChild because we already have an id and can use next urls: GET/DELETE/PUT /wheels/55
        /// TInputModel contains no Id field, TViewModel does
        /// </summary>
        /// <param name="key">This is a parrent item key</param>
        /// <param name="im"></param>
        /// <returns></returns>
        Task<TViewModel> CreateChildAsync(TParrentKey key, TInputModel im);
    }
}