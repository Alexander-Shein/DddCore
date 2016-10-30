using System.Threading.Tasks;

namespace Contracts.Services.Application.DomainStack
{
    public interface IUpdate<TVm, in TKey, in TIm>
    {
        Task<TVm> Update(TKey key, TIm model);
    }
}