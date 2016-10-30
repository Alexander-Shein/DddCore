using System.Threading.Tasks;

namespace Contracts.Services.Application.DomainStack
{
    public interface ICreateChild<TVm, in TParrentKey, in TIm>
    {
        Task<TVm> CreateChildAsync(TParrentKey key, TIm im);
    }
}