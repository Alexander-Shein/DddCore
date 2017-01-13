using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface IUpdateAsync<TViewModel, in TKey, in TInputModel>
    {
        /// <summary>
        /// Updates by key and returns ViewModel. InputModel contains no Id field, ViewMmodel does
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<TViewModel> UpdateAsync(TKey key, TInputModel model);
    }
}