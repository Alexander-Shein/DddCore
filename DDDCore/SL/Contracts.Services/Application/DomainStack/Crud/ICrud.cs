namespace Contracts.Services.Application.DomainStack.Crud
{
    public interface ICrud<TVm, in TKey, in TIm> :
        ICreate<TVm, TIm>,
        IRead<TVm, TKey>,
        IUpdate<TVm, TKey, TIm>,
        IDelete<TKey>
        where TIm : class
        where TVm : class
    {
    }
}