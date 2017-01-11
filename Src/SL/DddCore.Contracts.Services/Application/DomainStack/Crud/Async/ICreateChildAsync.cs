using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface ICreateChildAsync<TVm, in TParrentKey, in TIm>
    {
        Task<TVm> CreateChildAsync(TParrentKey key, TIm im);
    }
}