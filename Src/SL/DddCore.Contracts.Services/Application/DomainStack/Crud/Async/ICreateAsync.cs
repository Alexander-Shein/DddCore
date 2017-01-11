using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud.Async
{
    public interface ICreateAsync<TVm, in TIm>
    {
        Task<TVm> CreateAsync(TIm im);
    }
}
