namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface ICreateChild<out TViewModel, in TParrentKey, in TInputModel>
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
        TViewModel CreateChild(TParrentKey key, TInputModel im);
    }
}