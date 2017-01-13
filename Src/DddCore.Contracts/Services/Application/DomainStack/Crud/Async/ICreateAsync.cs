using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface ICreateAsync<TViewModel, in TInputModel>
    {
        /// <summary>
        /// Gets an InputModel and returns ViewModel. InputModel has no Id property, ViewModel does.
        /// </summary>
        /// <param name="im"></param>
        /// <returns></returns>
        Task<TViewModel> CreateAsync(TInputModel im);
    }
}
