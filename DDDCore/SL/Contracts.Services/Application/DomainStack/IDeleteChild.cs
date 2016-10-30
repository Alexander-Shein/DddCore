using System.Threading.Tasks;

namespace Contracts.Services.Application.DomainStack
{
    public interface IDeleteChild<in TParentKey, in TKey>
    {
        Task DeleteChildAsync(TParentKey parentKey, TKey key);
    }
}