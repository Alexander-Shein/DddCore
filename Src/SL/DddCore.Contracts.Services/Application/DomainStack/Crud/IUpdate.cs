using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface IUpdate<TVm, in TKey, in TIm>
    {
        Task<TVm> UpdateAsync(TKey key, TIm model);
    }
}