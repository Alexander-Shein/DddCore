using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface IUpdateAsync<TVm, in TKey, in TIm>
    {
        Task<TVm> UpdateAsync(TKey key, TIm model);
    }
}