using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IDeleteChild<in TParentKey, in TKey>
    {
        Task DeleteChildAsync(TParentKey parentKey, TKey key);
    }
}