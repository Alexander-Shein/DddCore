namespace DddCore.Contracts.SL.Services.Application.DomainStack.Crud.Async
{
    public interface ICrudAsync<TVm, in TKey, in TIm> :
        ICreateAsync<TVm, TIm>,
        IReadAsync<TVm, TKey>,
        ICreateOrUpdateAsync<TVm, TKey, TIm>,
        IDeleteAsync<TKey>
        where TIm : class
        where TVm : class
    {
    }
}
