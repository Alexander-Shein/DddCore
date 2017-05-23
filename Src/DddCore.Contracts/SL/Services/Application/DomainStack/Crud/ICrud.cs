namespace DddCore.Contracts.SL.Services.Application.DomainStack.Crud
{
    public interface ICrud<TViewModel, in TKey, in TInputModel> :
        ICreate<TViewModel, TInputModel>,
        IRead<TViewModel, TKey>,
        ICreateOrUpdate<TViewModel, TKey, TInputModel>,
        IDelete<TKey>
        where TInputModel : class
        where TViewModel : class
    {
    }
}