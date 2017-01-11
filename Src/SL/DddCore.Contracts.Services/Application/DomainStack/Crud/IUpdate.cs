namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IUpdate<out TViewModel, in TKey, in TInputModel>
    {
        /// <summary>
        /// Updates by key and returns ViewModel. InputModel contains no Id field, ViewMmodel does
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        TViewModel Update(TKey key, TInputModel model);
    }
}
