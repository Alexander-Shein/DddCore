namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface ICrud<out TVm, in TKey, in TIm> :
        ICreate<TVm, TIm>,
        IRead<TVm, TKey>,
        ICreateOrUpdate<TVm, TKey, TIm>,
        IDelete<TKey>
        where TIm : class
        where TVm : class
    {
    }
}