using DddCore.Contracts.BLL.Errors;

namespace DddCore.Contracts.SL.Services.Application.DomainStack.Crud
{
    public interface ICreateOrUpdate<TViewModel, in TKey, in TInputModel>
    {
        /// <summary>
        /// PUT /cars/{carId}/ HTTP/1.1.
        /// Updates by key or creates new entity with specified id and returns ViewModel.
        /// </summary>
        /// <param name="key">Id of entity that will be updated.</param>
        /// <param name="im">InputModel has no Id property because when we send request to update new object we pass id to url.</param>
        /// <returns>ViewModel contains Id property.</returns>
        (TViewModel Vm, OperationResult OperationResult) CreateOrUpdate(TKey key, TInputModel im);
    }
}